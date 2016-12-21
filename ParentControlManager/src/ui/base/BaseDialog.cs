using System.Diagnostics;
using System.Windows.Forms;

namespace ParentControlManager.UI.Base
{
    public partial class BaseDialog : Form
    {
        private bool? isDesignMode;

        public BaseDialog()
        {
            InitializeComponent();

            if (!IsDesignMode()) {
                this.btnOk.Text = ParentControlManager.Strings.GetString("ok");
                this.btnCancel.Text = ParentControlManager.Strings.GetString("cancel");
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
