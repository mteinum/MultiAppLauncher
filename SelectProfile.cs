using System;
using System.Configuration;
using System.Windows.Forms;

namespace MultiAppLauncher
{
    public partial class SelectProfile : Form
    {
        public string SelectedProfile { get; private set; }

        public SelectProfile()
        {
            InitializeComponent();
        }

        private void SelectProfile_Load(object sender, EventArgs e)
        {
            var profiles = ConfigurationManager.AppSettings["Profiles"];

            if (!String.IsNullOrEmpty(profiles))
            {
                listBox1.Items.AddRange(
                    profiles.Split(',')
                );
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                SelectedProfile = (string)listBox1.SelectedItem;
            }

        }
    }
}
