using ParentControl.ObjectModel;
using ParentControlManager.Model;
using ParentControlManager.Remote;
using ParentControlManager.UI.Base;
using System;
using System.Threading;

namespace ParentControlManager.UI
{
    public partial class RemoteInformationDialog : BaseDialog
    {
        private Connection connection;
        private volatile object synobj = new object();

        /// <summary>
        /// Default public constructor.
        /// </summary>
        /// <param name="connection"></param>
        public RemoteInformationDialog(Connection connection) : this()
        {
            this.connection = connection;
            RefreshData();
        }

        private RemoteInformationDialog()
        {
            InitializeComponent();
            this.Text = ParentControlManager.Strings.GetString("remote_information");
            this.lblActualDate.Text = ParentControlManager.Strings.GetString("actual_date");
            this.lblDuration.Text = ParentControlManager.Strings.GetString("duration");
            this.btnReset.Text = ParentControlManager.Strings.GetString("reset_duration");
            timer1.Start();
        }

        /// <summary>
        /// Refresh data on dialog.
        /// </summary>
        private void RefreshData()
        {
            lock (synobj)
            {
                ObservedValuesRemote remote = new ObservedValuesRemote(connection.Hostname, connection.Port.Value);
                ObservedValues ov = remote.GetObservedValues();

                txtActualDate.Text =
                    (ov.ActualDate != null
                    ? ov.ActualDate.Value.ToString(Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern)
                    : "");
                txtDuration.Text = "";
                if (ov.Duration != null)
                {
                    TimeSpan t = TimeSpan.FromMilliseconds(ov.Duration.Value);
                    string time = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms", t.Hours, t.Minutes, t.Seconds, t.Milliseconds);
                    txtDuration.Text = time;
                }
            }
        }

        /// <summary>
        /// Click on reset button send request to reset duration.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            ObservedValuesRemote remote = new ObservedValuesRemote(connection.Hostname, connection.Port.Value);
            remote.Reset();
            RefreshData();
        }

        /// <summary>
        /// Interval for refreshing data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            RefreshData();
        }

        /// <summary>
        /// On closing form we need stop the timer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoteInformationDialog_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            timer1.Stop();
            timer1.Dispose();
        }
    }

}
