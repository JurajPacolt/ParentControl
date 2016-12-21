/* Created on 29.10.2016 */
using ParentControlManager.Model;
using ParentControlManager.Provider;
using ParentControlManager.UI.Base;

namespace ParentControlManager.UI
{
    public partial class StationDetailDialog : BaseDialog
    {
        private int? id = null;
        private ConnectionDataProvider provider = new ConnectionDataProvider();

        /// <summary>
        /// Constructor for opening exists record.
        /// </summary>
        /// <param name="id"></param>
        public StationDetailDialog(int id) : this()
        {
            this.id = id;

            Connection conn = provider.GetConnection(id);
            if (conn != null)
            {
                txtName.Text = conn.Name;
                txtIpAddressHostname.Text = conn.Hostname;
                nudPort.Value = conn.Port.Value;
            }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public StationDetailDialog()
        {
            InitializeComponent();
            lblName.Text = ParentControlManager.Strings.GetString("name");
            lblIpAddressHostname.Text = ParentControlManager.Strings.GetString("ip_address_hostname");
            lblPort.Text = ParentControlManager.Strings.GetString("port");
        }

        /// <summary>
        /// Storing object to DB.
        /// </summary>
        public void Save()
        {
            Connection conn = new Connection();
            conn.Id = this.id;
            conn.Name = txtName.Text;
            conn.Hostname = txtIpAddressHostname.Text;
            conn.Port = (int?)nudPort.Value;

            provider.StoreConnection(conn);
        }

        /// <summary>
        /// Click to confirm button (OK).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, System.EventArgs e)
        {
            Save();
        }
    }

}
