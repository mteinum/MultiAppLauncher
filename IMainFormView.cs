using System.Collections.Generic;
using System.Windows.Forms;

namespace MultiAppLauncher
{
    public interface IMainFormView : IWin32Window
    {
        int SoftStartSeconds { get; set; }
        
        List<ListViewItem> GetListViewItems();
        List<ListViewItem> GetSelectedItems();

        void SetListViewItem(ListViewItem lvi, int column, string text);

        void AddListViewItem(ListViewItem lvi);
        void ClearListView();

        void SetToolStripProgressBar(int value);
        string ToolStripFileName { get; set; }
    }
}