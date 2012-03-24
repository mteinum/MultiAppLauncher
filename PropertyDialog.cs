using System;
using System.Globalization;
using System.Windows.Forms;

namespace MultiAppLauncher
{
    public partial class PropertyDialog : Form
    {
        public PropertyDialog()
        {
            InitializeComponent();
        }

        public int Timeout
        {
            get
            {
                try
                {
                    return Int32.Parse(textBoxDelay.Text);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            set { textBoxDelay.Text = value.ToString(CultureInfo.InvariantCulture); }
        }
    }
}
