using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MultiAppLauncher.Properties;

namespace MultiAppLauncher
{
    public partial class MainForm : Form, IMainFormView
    {
        private readonly StorageController _storageController;
        private readonly CpuController _cpuController;
        private readonly ProcessController _processController;
        private readonly DragDropController _dragDropController;
        private readonly ProfileController _profileController;

        public MainForm()
        {
            InitializeComponent();

            _storageController = new StorageController(this);
            _cpuController = new CpuController(this);
            _processController = new ProcessController(this);
            _dragDropController = new DragDropController(this);
            _profileController = new ProfileController(this);

            SoftStartSeconds = 2;

            SetToolStripFileName(String.Empty);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            _dragDropController.DragDrop(e);
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            _dragDropController.DragEnter(e);
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _processController.StartProcesses();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _storageController.SaveAs();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _storageController.Save();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _storageController.Open();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Resources.AboutText, Resources.AboutCaption);
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            _processController.BringSelectedAppToFront();
        }

        private void killAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _processController.KillAll();
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            _profileController.ChangeProfile(e);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _storageController.ChangeProperties();
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                foreach (ListViewItem lvi in GetSelectedItems())
                {
                    lvi.Remove();
                }
            }
        }

        private int _softStartSeconds;

        public int SoftStartSeconds
        {
            get { return _softStartSeconds; }
            set
            {
                _softStartSeconds = value;
                toolStripStatusLabelSoftStart.Text = String.Format("Soft start: {0} sec", value);
            }
        }

        public IEnumerable<ListViewItem> GetListViewItems()
        {
            if (InvokeRequired)
            {
                IEnumerable<ListViewItem> result = null;
                Invoke(new MethodInvoker(() => result = GetListViewItems().ToList()));
                return result;
            }

            return listView1.Items.Cast<ListViewItem>();
        }

        public IEnumerable<ListViewItem> GetSelectedItems()
        {
            if (InvokeRequired)
            {
                IEnumerable<ListViewItem> result = null;
                Invoke(new MethodInvoker(() => result = GetSelectedItems().ToList()));
                return result;
            }

            return listView1.SelectedItems.Cast<ListViewItem>();
        }

        public void SetListViewItem(ListViewItem lvi, int column, string text)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => SetListViewItem(lvi, column, text)));
            }
            else
            {
                lvi.SubItems[column].Text = text;
            }
        }

        public void AddListViewItem(ListViewItem lvi)
        {
            listView1.Items.Add(lvi);
        }

        public void ClearListView()
        {
            listView1.Items.Clear();
        }

        public void SetToolStripProgressBar(int value)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => SetToolStripProgressBar(value)));
            }
            else
            {
                toolStripProgressBar1.Value = value;
            }
        }

        public void SetToolStripFileName(string fileName)
        {
            toolStripStatusLabelFileName.Text = fileName;
        }
    }
}
