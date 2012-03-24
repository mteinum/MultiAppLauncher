using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiAppLauncher
{
    public partial class FileScanner : Form
    {
        public FileScanner()
        {
            InitializeComponent();
        }

        public string SelectedPath
        {
            get { return textBoxFolder.Text; }
            set { textBoxFolder.Text = value; }
        }

        public string Pattern
        {
            get { return textBoxPattern.Text; }
            set { textBoxPattern.Text = value; }
        }
    }
}
