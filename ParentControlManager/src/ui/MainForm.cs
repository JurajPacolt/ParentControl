/* Created on 22.10.2016 */
using ParentControlManager.Model;
using ParentControlManager.Properties;
using ParentControlManager.Provider;
using ParentControlManager.UI;
using System.Windows.Forms;

namespace ParentControlManager
{
    public partial class MainForm : Form
    {
        private const string CM_EDIT = "EDIT";
        private const string CM_DELETE = "DELETE";
        private const string CM_REMOTE_INFORMATION = "REMOTE_INFORMATION";

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            this.Text = ParentControlManager.Strings.GetString("application_title");
            this.btnAddStation.Text = ParentControlManager.Strings.GetString("add_station");
            this.btnEditStation.Text = ParentControlManager.Strings.GetString("edit_station");
            this.btnDeleteStation.Text = ParentControlManager.Strings.GetString("delete_station");

            listViewMain.MouseClick += ListViewMain_MouseClick;
            listViewMain.MouseDoubleClick += ListViewMain_MouseDoubleClick;
            listViewMain.KeyDown += ListViewMain_KeyDown;

            // Context menu - click on item events.
            cmsMenuItems.Items[0].Text = ParentControlManager.Strings.GetString("edit_station");
            cmsMenuItems.Items[1].Text = ParentControlManager.Strings.GetString("delete_station");
            cmsMenuItems.Items[3].Text = ParentControlManager.Strings.GetString("remote_information");
            cmsMenuItems.ItemClicked += CmsMenuItems_ItemClicked;

            RefreshData();
        }

        /// <summary>
        /// Clicked on contex menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmsMenuItems_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem != null)
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case CM_EDIT:
                        ModifyConnection((int)((Connection)listViewMain.SelectedItems[0].Tag).Id);
                        break;
                    case CM_DELETE:
                        DeleteItem();
                        break;
                    case CM_REMOTE_INFORMATION:
                        ShowRemoteInformaton((Connection)listViewMain.SelectedItems[0].Tag);
                        break;
                }
            }
        }

        /// <summary>
        /// Show remote information about connection.
        /// </summary>
        private void ShowRemoteInformaton(Connection connection)
        {
            RemoteInformationDialog dlg = new RemoteInformationDialog(connection);
            dlg.ShowDialog(this);
            // TODO Dopracovat zobrazenie remote informacii.
        }

        /// <summary>
        /// Mouse click, here is applied right click for context menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1 && e.Button == MouseButtons.Right)
            {
                cmsMenuItems.Show(listViewMain, e.X, e.Y);
            }
        }

        /// <summary>
        /// Key down event. On ENTER or SPACE key, it's opening detail of the connection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewMain_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space) && listViewMain.SelectedItems.Count > 0)
            {
                ModifyConnection((int)((Connection)listViewMain.SelectedItems[0].Tag).Id);
            }
        }

        /// <summary>
        /// Double click event for opening detial of the connection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2 && e.Button == MouseButtons.Left && listViewMain.SelectedItems.Count > 0)
            {
                ModifyConnection((int)((Connection)listViewMain.SelectedItems[0].Tag).Id);
            }
        }

        /// <summary>
        /// Refresh of the list view component.
        /// </summary>
        public void RefreshData()
        {
            ConnectionDataProvider provider = new ConnectionDataProvider();

            listViewMain.Clear();
            foreach (Connection conn in provider.GetConnectionList())
            {
                ListViewItem item = new ListViewItem(conn.Name, 0);
                item.Tag = conn;
                item.ToolTipText = conn.Name;
                listViewMain.Items.Add(item);
            }

            base.Refresh();
        }

        /// <summary>
        /// Click to add station to the listview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddStation_Click(object sender, System.EventArgs e)
        {
            AddConnection();
        }

        private void AddConnection()
        {
            StationDetailDialog dialog = new StationDetailDialog();
            dialog.Text = ParentControlManager.Strings.GetString("add_station") + " ...";
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                RefreshData();
            }
        }

        /// <summary>
        /// Modify connection record.
        /// </summary>
        /// <param name="id"></param>
        private void ModifyConnection(int id)
        {
            StationDetailDialog dialog = new StationDetailDialog(id);
            dialog.Text = ParentControlManager.Strings.GetString("edit_station") + " ...";
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                RefreshData();
            }
        }

        /// <summary>
        /// Click to modification of the station.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditStation_Click(object sender, System.EventArgs e)
        {
            if (listViewMain.SelectedItems.Count > 0)
            {
                ModifyConnection((int)((Connection)listViewMain.SelectedItems[0].Tag).Id);
            }
            else
            {
                MessageBox.Show(
                    this, ParentControlManager.Strings.GetString("must_select_record"),
                    ParentControlManager.Strings.GetString("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Click on delete item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteStation_Click(object sender, System.EventArgs e)
        {
            if (listViewMain.SelectedItems.Count > 0)
            {
                DeleteItem();
            }
            else
            {
                MessageBox.Show(
                    this, ParentControlManager.Strings.GetString("must_select_record"),
                    ParentControlManager.Strings.GetString("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DeleteItem()
        {
            if (MessageBox.Show(
                this, ParentControlManager.Strings.GetString("really_delete_record"),
                ParentControlManager.Strings.GetString("question"), MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                ConnectionDataProvider provider = new ConnectionDataProvider();
                provider.DeleteConnection((int)((Connection)listViewMain.SelectedItems[0].Tag).Id);
                RefreshData();
            }
        }

    }

}
