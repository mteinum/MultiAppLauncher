using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using MultiAppLauncher.Properties;

namespace MultiAppLauncher
{
    public partial class MainForm : Form
    {
        private const int ProfileColumn = 1;
        private const int StatusColumn = 2;

        public MainForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AddFileNameToList(string fileName, string profile)
        {
            var x = new ListViewItem(new[]
                                         {
                                             fileName,
                                             profile,
                                             "---"
                                         });

            listView1.Items.Add(x);
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            var fileNames = e.Data.GetData("FileNameW") as string[];

            if (fileNames == null || fileNames.Length == 0)
                return;

            var profile = new SelectProfile();

            if (profile.ShowDialog(this) == DialogResult.OK)
            {
                AddFileNameToList(fileNames[0], profile.SelectedProfile);
            }
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("FileNameW"))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private IEnumerable GetItemsToExecute()
        {
            if (listView1.SelectedItems.Count == 0)
                return listView1.Items;
            return listView1.SelectedItems;
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // execute all processes in the listbox
            foreach (ListViewItem item in GetItemsToExecute())
            {
                if (item.Tag == null)
                {

                    var process = new Process
                    {
                        StartInfo =
                            {
                                FileName = item.Text,
                                UseShellExecute = true,
                                Arguments = item.SubItems[ProfileColumn].Text
                            },
                        EnableRaisingEvents = true
                    };

                    process.Exited += ProcessOnExited;
                    process.Start();

                    item.Tag = process;
                    item.SubItems[StatusColumn].Text = Resources.StatusRunning;
                }
            }
        }

        private void ProcessOnExited(object sender, EventArgs eventArgs)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                var process = (Process)item.Tag;

                if (process == null)
                {
                    item.SubItems[StatusColumn].Text = String.Empty;
                }
                else if (process.HasExited)
                {
                    item.SubItems[StatusColumn].Text = String.Empty;
                    item.Tag = null;
                }
                else
                {
                    item.SubItems[StatusColumn].Text = Resources.StatusRunning;
                }
            }
        }

        private string _currentFileName;

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentFileName = null;

            saveToolStripMenuItem_Click(sender, e);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // if currentfilename is empty; ask for a new one
            if (String.IsNullOrEmpty(_currentFileName))
            {
                var sfd = new SaveFileDialog
                              {
                                  DefaultExt = ".xml",
                                  Filter = Resources.DialogFilter
                              };

                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    Text = _currentFileName = sfd.FileName;
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

                    MessageBox.Show(this, builder.ToString(), Resources.CaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private SettingsDocument CreateSettingsDocument()
        {
            var doc = new SettingsDocument { FileNames = new List<FileSettings>() };

            foreach (ListViewItem item in listView1.Items)
            {
                doc.FileNames.Add(new FileSettings
                                      {
                                          Name = item.Text,
                                          Profile = item.SubItems[ProfileColumn].Text
                                      });
            }

            return doc;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = Resources.DialogFilter };

            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                _currentFileName = Text = ofd.FileName;

                var settings = SettingsDocument.Load(_currentFileName);

                listView1.Items.Clear();

                foreach (var entry in settings.FileNames)
                {
                    AddFileNameToList(entry.Name, entry.Profile);
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Resources.AboutText, Resources.AboutCaption);
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 1)
                return;

            var selectedItem = listView1.SelectedItems[0];

            var dlg = new SelectProfile();

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                selectedItem.SubItems[ProfileColumn].Text = dlg.SelectedProfile;
            }
        }
    }
}
