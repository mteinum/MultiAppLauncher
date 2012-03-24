using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MultiAppLauncher.Properties;

namespace MultiAppLauncher
{
    public class StorageController
    {
        private string _currentFileName;

        private readonly IMainFormView _view;

        public StorageController(IMainFormView view)
        {
            _view = view;
        }

        public void SaveAs()
        {
            _currentFileName = null;
            Save();
        }

        public void Save()
        {
            if (String.IsNullOrEmpty(_currentFileName))
            {
                var sfd = new SaveFileDialog
                              {
                                  DefaultExt = ".xml",
                                  Filter = Resources.DialogFilter
                              };

                if (sfd.ShowDialog(_view) == DialogResult.OK)
                {
                    _currentFileName = sfd.FileName;
                    _view.SetToolStripFileName(_currentFileName);
                }
            }

            if (!String.IsNullOrEmpty(_currentFileName))
            {
                try
                {
                    CreateSettingsDocument().Save(_currentFileName);
                }
                catch (Exception ex)
                {
                    _currentFileName = null;

                    var builder = new StringBuilder();
                    builder.AppendFormat("Failed to write file. Try another filename. {0}", ex);

                    MessageBox.Show(_view,
                                    builder.ToString(),
                                    Resources.CaptionError,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }

        private SettingsDocument CreateSettingsDocument()
        {
            var doc = new SettingsDocument
                          {
                              SoftStart = _view.SoftStartSeconds,
                              FileNames = new List<FileSettings>()
                          };

            foreach (ListViewItem item in _view.GetListViewItems())
            {
                doc.FileNames.Add(new FileSettings
                                      {
                                          Name = item.Text,
                                          Profile = item.SubItems[Columns.Profile].Text
                                      });
            }

            return doc;
        }

        public void Open()
        {
            var ofd = new OpenFileDialog { Filter = Resources.DialogFilter };

            if (ofd.ShowDialog(_view) == DialogResult.OK)
            {
                _currentFileName = ofd.FileName;
                _view.SetToolStripFileName(_currentFileName);

                var settings = SettingsDocument.Load(_currentFileName);

                _view.SoftStartSeconds = settings.SoftStart;

                _view.ClearListView();

                foreach (var entry in settings.FileNames)
                {
                    AddFileNameToList(entry.Name, entry.Profile);
                }
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

        public void ChangeProperties()
        {
            var dlg = new PropertyDialog { Timeout = _view.SoftStartSeconds };

            if (dlg.ShowDialog(_view) == DialogResult.OK)
            {
                _view.SoftStartSeconds = dlg.Timeout;
            }
        }
    }
}