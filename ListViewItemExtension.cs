using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MultiAppLauncher
{
    public static class ListViewItemExtension
    {
        public static ProcessHolder GetProcessHolder(this ListViewItem lvi)
        {
            return lvi.Tag as ProcessHolder;
        }

        public static IEnumerable<ListViewItem> GetItemsToExecute(this ListView lv)
        {
            if (lv.SelectedItems.Count == 0)
                return lv.Items.Cast<ListViewItem>();

            return lv.SelectedItems.Cast<ListViewItem>();
        }
    }
}