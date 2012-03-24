using System.Linq;
using System.Windows.Forms;

namespace MultiAppLauncher
{
    public class ProfileController
    {
        private readonly IMainFormView _view;

        public ProfileController(IMainFormView view)
        {
            _view = view;
        }

        public void ChangeProfile(MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
            {
                var selectedItem = _view.GetSelectedItems().FirstOrDefault();

                if (selectedItem == null)
                    return;

                var dlg = new SelectProfile();

                if (dlg.ShowDialog(_view) == DialogResult.OK)
                {
                    _view.SetListViewItem(selectedItem, Columns.Profile, dlg.SelectedProfile);
                }
            }
        }
    }
}