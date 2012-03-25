using System;
using System.Collections;
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
        private readonly WindowController _windowController;

        public MainForm()
        {
            InitializeComponent();

            _storageController = new StorageController(this);
            _cpuController = new CpuController(this);
            _processController = new ProcessController(this);
            _dragDropController = new DragDropController(this);
            _profileController = new ProfileController(this);
            _windowController = new WindowController(this);

            SoftStartSeconds = 2;
            ToolStripFileName = String.Empty;

            _processController.AllProcessesStarted += AllProcessesStarted;
        }

        private void AllProcessesStarted(object sender, EventArgs eventArgs)
        {
            SafeSet(() => _windowController.AutoLayout(Handle));
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

        public string ToolStripFileName
        {
            get { return toolStripStatusLabelFileName.Text; }
            set { toolStripStatusLabelFileName.Text = value; }
        }

        public List<ListViewItem> GetListViewItems()
        {
            return SafeGet(() => listView1.Items);
        }

        public List<ListViewItem> GetSelectedItems()
        {
            return SafeGet(() => listView1.SelectedItems);
        }

        public void SetListViewItem(ListViewItem lvi, int column, string text)
        {
            SafeSet(() => lvi.SubItems[column].Text = text);
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
            SafeSet(() => toolStripProgressBar1.Value = value);
        }

        private void SafeSet(Action action)
        {
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }

        private List<ListViewItem> SafeGet(Func<IEnumerable> func)
        {
            if (InvokeRequired)
            {
                List<ListViewItem> result = null;
                Invoke(new MethodInvoker(delegate { result = SafeGet(func); }));
                return result;
            }

            return func().Cast<ListViewItem>().ToList();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _cpuController.Stop();

            base.OnFormClosing(e);
        }

        private void layoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _windowController.AutoLayout(Handle);
        }
    }
}
