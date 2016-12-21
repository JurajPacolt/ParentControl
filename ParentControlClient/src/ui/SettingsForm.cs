/* Created on 9.10.2016 */
using ParentControl.ObjectModel;
using ParentControlClient.Remote;
using ParentControlClient.UI.Base;
using System;
using System.Windows.Forms;

namespace ParentControlClient.UI
{
    public partial class SettingsForm : BaseDialog
    {
        private BaseSettings baseSettings;

        public SettingsForm(BaseSettings baseSettings) : base()
        {
            this.baseSettings = baseSettings;
            InitializeComponent();
            this.Text = ParentControlClient.Strings.GetString("settings");
            this.label1.Text = ParentControlClient.Strings.GetString("shutdown_command");
            this.label2.Text = ParentControlClient.Strings.GetString("check_interval");
            this.label3.Text = ParentControlClient.Strings.GetString("service_url");
            this.label4.Text = ParentControlClient.Strings.GetString("delay_start");

            ReadFromObject();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            txtShutdownCommand.Focus();
        }

        /// <summary>
        /// Writing values from fields to object.
        /// </summary>
        private void WriteFieldsToObject()
        {
            baseSettings.ShutdownCommand = !string.IsNullOrWhiteSpace(txtShutdownCommand.Text) ? txtShutdownCommand.Text.Trim() : null;
            baseSettings.CheckInterval = nudCheckInterval.Value > 0 ? (int?)nudCheckInterval.Value : null;
            baseSettings.UrlService = !string.IsNullOrWhiteSpace(txtServiceUrl.Text) ? txtServiceUrl.Text.Trim() : null;
            baseSettings.DelayStart = nudDelayStart.Value > 0 ? (int?)nudDelayStart.Value : null;
        }

        /// <summary>
        /// Reading data from object to fields.
        /// </summary>
        private void ReadFromObject()
        {
            this.txtShutdownCommand.Text = baseSettings.ShutdownCommand != null ? baseSettings.ShutdownCommand : "";
            this.nudCheckInterval.Value = baseSettings.CheckInterval != null ? baseSettings.CheckInterval.Value : 0;
            this.txtServiceUrl.Text = baseSettings.UrlService != null ? baseSettings.UrlService : "";
            this.nudDelayStart.Value = baseSettings.DelayStart != null ? baseSettings.DelayStart.Value : 0;
        }

        /// <summary>
        /// Click event for OK button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            WriteFieldsToObject();
            BaseSettingsRemote remote = new BaseSettingsRemote();
            try {
                remote.StoreBaseSettings(baseSettings);
            }
            catch (System.Exception ex) {
                this.DialogResult = DialogResult.None;
                throw ex;
            }
        }
    }

}
