using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using MultiAppLauncher.Properties;

namespace MultiAppLauncher
{
    public class ProcessController
    {
        private readonly IMainFormView _view;
        private readonly ITaskbarList _taskbarList;

        private readonly Thread _startProcessThread;
        private bool _stopping;
        private readonly AutoResetEvent _startProcessingEvent;

        public ProcessController(IMainFormView view)
        {
            Application.ApplicationExit += ApplicationOnApplicationExit;

            _taskbarList = (ITaskbarList)new CoTaskbarList();
            _taskbarList.HrInit();

            _view = view;

            _startProcessingEvent = new AutoResetEvent(false);
            _startProcessThread = new Thread(StartProcessWorker);
            _startProcessThread.Start();
        }

        private void ApplicationOnApplicationExit(object sender, EventArgs eventArgs)
        {
            _stopping = true;
            _startProcessingEvent.Set();
            _startProcessThread.Join();
        }

        public void KillAll()
        {

            foreach (ListViewItem lvi in _view.GetListViewItems())
            {
                var holder = lvi.GetProcessHolder();

                if (holder == null)
                    continue;

                try
                {
                    holder.Process.Kill();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(_view, String.Format(
                        "Failed to kill {0}. {1}", holder.Process.ProcessName, ex));
                }
            }
        }

        public void BringSelectedAppToFront()
        {
            var selectedItem = _view.GetSelectedItems().FirstOrDefault();

            if (selectedItem == null)
                return;

            var p = selectedItem.GetProcessHolder();

            if (p == null)
                return;

            SetForegroundWindow(p.Process.MainWindowHandle.ToInt32());
        }

        [DllImport("User32.dll")]
        public static extern Int32 SetForegroundWindow(int hWnd);

        private IEnumerable<ListViewItem> GetItemsToExecute()
        {
            var items = _view.GetSelectedItems().ToList();

            if (!items.Any())
                items = _view.GetListViewItems().ToList();

            return items;
        }

        private void StartProcessWorker()
        {
            while (true)
            {
                _startProcessingEvent.WaitOne();

                if (_stopping)
                    break;

                foreach (var lvi in GetItemsToExecute().Where(t => t.Tag == null))
                {
                    var holder = new ProcessHolder
                    {
                        Process = new Process
                        {
                            StartInfo =
                            {
                                FileName = lvi.Text,
                                UseShellExecute = true,
                                Arguments = lvi.SubItems[Columns.Profile].Text
                            },
                            EnableRaisingEvents = true
                        }
                    };

                    _view.SetToolStripProgressBar(0);

                    holder.Process.Exited += ProcessOnExited;
                    holder.Process.Start();

                    WaitForMainWindow(holder.Process);

                    holder.CpuUsage = holder.Process.TotalProcessorTime.TotalMilliseconds;

                    ModifyProcess(holder.Process.MainWindowHandle);

                    lvi.Tag = holder;
                    _view.SetListViewItem(lvi, Columns.Status, Resources.StatusRunning);
                    _view.SetListViewItem(lvi, Columns.Cpu, 0d.ToString("P"));

                    double totalMs = _view.SoftStartSeconds * 1000d;
                    TimeSpan wait = TimeSpan.FromMilliseconds(totalMs / 10d);

                    for (int i = 0; i < 100; i += 10)
                    {
                        _view.SetToolStripProgressBar(i);
                        Thread.Sleep(wait);
                    }

                    _view.SetToolStripProgressBar(0);
                }
            }
        }

        public void StartProcesses()
        {
            _startProcessingEvent.Set();
        }

        private static void WaitForMainWindow(Process process)
        {
            while (process.MainWindowHandle == IntPtr.Zero && !process.HasExited)
                Thread.Sleep(10);
        }

        private void ModifyProcess(IntPtr hWnd)
        {
            _taskbarList.DeleteTab(hWnd);
        }

        private void ProcessOnExited(object sender, EventArgs eventArgs)
        {
            foreach (var item in _view.GetListViewItems())
            {
                var process = (ProcessHolder)item.Tag;

                if (process == null)
                {
                    _view.SetListViewItem(item, Columns.Status, String.Empty);
                    _view.SetListViewItem(item, Columns.Cpu, String.Empty);
                }
                else if (process.Process.HasExited)
                {
                    _view.SetListViewItem(item, Columns.Status, String.Empty);
                    _view.SetListViewItem(item, Columns.Cpu, String.Empty);
                    item.Tag = null;
                }
                else
                {
                    _view.SetListViewItem(item, Columns.Status, Resources.StatusRunning);
                }
            }
        }


    }
}