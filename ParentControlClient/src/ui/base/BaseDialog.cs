using System.Diagnostics;
using System.Windows.Forms;

namespace ParentControlClient.UI.Base
{
    public partial class BaseDialog : Form
    {
        private bool? isDesignMode;

        public BaseDialog()
        {
            InitializeComponent();

            if (!IsDesignMode()) {
                this.btnOk.Text = ParentControlClient.Strings.GetString("ok");
                this.btnCancel.Text = ParentControlClient.Strings.GetString("cancel");
            }
        }

        private bool IsDesignMode()
        {
            if (isDesignMode == null)
                isDesignMode = (Process.GetCurrentProcess().ProcessName.ToLower().Contains("devenv"));

            return isDesignMode.Value;
        }

    }
}
