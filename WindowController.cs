using System;
using System.Linq;
using System.Windows.Forms;

namespace MultiAppLauncher
{
    public class WindowController
    {
        private readonly IMainFormView _view;

        public WindowController(IMainFormView view)
        {
            _view = view;
        }


        public void AutoLayout(IntPtr mainWindowHandle)
        {
            var screenRect = Screen.PrimaryScreen.WorkingArea;

            var windows = _view.GetListViewItems()
                .Where(t => t.Tag != null)
                .Select(y => y.GetProcessHolder().Process.MainWindowHandle)
                .ToList();

            if (windows.Any())
            {
                int columns = (int)Math.Sqrt(windows.Count);
                int rows = windows.Count / columns;

                if (rows * columns < windows.Count)
                    rows++;

                int windowWidth = screenRect.Width / columns;
                int windowHeigth = screenRect.Height / rows;

                int i = 0;

                for (int y = 0; y < rows; y++)
                {
                    for (int x = 0; x < columns && i < windows.Count; x++, i++)
                    {
                        Unmanaged.SetWindowPos(
                            windows[i],
                            Unmanaged.HWND_TOPMOST,
                            x * windowWidth,
                            y * windowHeigth,
                            windowWidth,
                            windowHeigth,
                            SetWindowPosFlags.SWP_SHOWWINDOW | SetWindowPosFlags.SWP_NOZORDER);
                    }
                }
            }

            // bring this window to front
            Unmanaged.RECT mainWindowRect;
            Unmanaged.GetWindowRect(mainWindowHandle, out mainWindowRect);

            Unmanaged.SetWindowPos(
                mainWindowHandle,
                Unmanaged.HWND_TOPMOST,
                screenRect.Width - mainWindowRect.Width,
                screenRect.Height - mainWindowRect.Height,
                mainWindowRect.Width,
                mainWindowRect.Height,
                SetWindowPosFlags.SWP_SHOWWINDOW);

        }
    }
}