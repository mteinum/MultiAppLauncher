using System;
using System.Windows.Forms;

namespace MultiAppLauncher
{
    public class DragDropController
    {
        private readonly IMainFormView _view;

        public DragDropController(IMainFormView view)
        {
            _view = view;
        }

        public void DragDrop(DragEventArgs e)
        {
            var fileNames = e.Data.GetData("FileNameW") as string[];

            if (fileNames == null || fileNames.Length == 0)
                return;

            var profile = new SelectProfile();

            if (profile.ShowDialog(_view) == DialogResult.OK)
            {
                AddFileNameToList(fileNames[0], profile.SelectedProfile);
            }
        }

        public void DragEnter(DragEventArgs e)
        {
            if (e.Data.GetDataPresent("FileNameW"))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void AddFileNameToList(string fileName, string profile)
        {
            _view.AddListViewItem(new ListViewItem(new[]
                                                       {
                                                           fileName,
                                                           profile,
                                                           String.Empty,
                                                           String.Empty
                                                       }));
        }

    }
}