/* Created on 26.9.2016 */
using ParentControl.ObjectModel;
using ParentControlClient.Remote;
using ParentControlClient.UI.Base;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace ParentControlClient.UI
{
    /// <summary>
    /// Detail of the rule.
    /// </summary>
    public partial class RuleDetailForm : BaseDialog
    {
        private Rule rule;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="rule"></param>
        public RuleDetailForm(Rule rule) : base()
        {
            if (rule == null) {
                throw new InvalidOperationException("Parameter 'rule' can not be NULL.");
            }

            this.rule = rule;

            InitializeComponent();
            this.Text = ParentControlClient.Strings.GetString("new_rule");
            this.lblName.Text = ParentControlClient.Strings.GetString("name");
            this.lblFromDateAndTime.Text = ParentControlClient.Strings.GetString("from_date_and_time");
            this.lblToDateAndTime.Text = ParentControlClient.Strings.GetString("to_date_and_time");
            this.lblDuration.Text = ParentControlClient.Strings.GetString("duration") + " [" + ParentControlClient.Strings.GetString("minutes") + "]";
            this.lblDayOfWeek.Text = ParentControlClient.Strings.GetString("day_of_week");
            this.lblEnabled.Text = ParentControlClient.Strings.GetString("enabled");

            // Days of the week
            this.cboDayOfWeek.Items.Clear();
            if (Thread.CurrentThread.CurrentCulture.Name == "sk-SK") {
                this.cboDayOfWeek.Items.Add(new ParentControl.ObjectModel.DayOfWeek(ParentControlClient.Strings.GetString("monday"), 1));
                this.cboDayOfWeek.Items.Add(new ParentControl.ObjectModel.DayOfWeek(ParentControlClient.Strings.GetString("tuesday"), 2));
                this.cboDayOfWeek.Items.Add(new ParentControl.ObjectModel.DayOfWeek(ParentControlClient.Strings.GetString("wednesday"), 3));
                this.cboDayOfWeek.Items.Add(new ParentControl.ObjectModel.DayOfWeek(ParentControlClient.Strings.GetString("thursday"), 4));
                this.cboDayOfWeek.Items.Add(new ParentControl.ObjectModel.DayOfWeek(ParentControlClient.Strings.GetString("friday"), 5));
                this.cboDayOfWeek.Items.Add(new ParentControl.ObjectModel.DayOfWeek(ParentControlClient.Strings.GetString("saturday"), 6));
                this.cboDayOfWeek.Items.Add(new ParentControl.ObjectModel.DayOfWeek(ParentControlClient.Strings.GetString("sunday"), 0));
            } else {
                this.cboDayOfWeek.Items.Add(new ParentControl.ObjectModel.DayOfWeek(ParentControlClient.Strings.GetString("sunday"), 0));
                this.cboDayOfWeek.Items.Add(new ParentControl.ObjectModel.DayOfWeek(ParentControlClient.Strings.GetString("monday"), 1));
                this.cboDayOfWeek.Items.Add(new ParentControl.ObjectModel.DayOfWeek(ParentControlClient.Strings.GetString("tuesday"), 2));
                this.cboDayOfWeek.Items.Add(new ParentControl.ObjectModel.DayOfWeek(ParentControlClient.Strings.GetString("wednesday"), 3));
                this.cboDayOfWeek.Items.Add(new ParentControl.ObjectModel.DayOfWeek(ParentControlClient.Strings.GetString("thursday"), 4));
                this.cboDayOfWeek.Items.Add(new ParentControl.ObjectModel.DayOfWeek(ParentControlClient.Strings.GetString("friday"), 5));
                this.cboDayOfWeek.Items.Add(new ParentControl.ObjectModel.DayOfWeek(ParentControlClient.Strings.GetString("saturday"), 6));
            }

            ReadFromObject();
            this.txtName.Focus();
        }

        /// <summary>
        /// Writing values from fields to object.
        /// </summary>
        private void WriteFieldsToObject()
        {
            rule.Name = !string.IsNullOrEmpty(this.txtName.Text.Trim()) ? this.txtName.Text.Trim() : null;
            rule.FromDateTime = this.dtpFromDateAndTime.Checked ? (DateTime?)dtpFromDateAndTime.Value : null;
            rule.ToDateTime = this.dtpToDateAndTime.Checked ? (DateTime?)dtpToDateAndTime.Value : null;
            rule.DurationInMinutes = (int?)this.nudDuration.Value;
            rule.DayOfWeek = this.cboDayOfWeek.SelectedItem != null ? (int?)((ParentControl.ObjectModel.DayOfWeek)this.cboDayOfWeek.SelectedItem).Id : null;
            rule.Enabled = this.chkEnabled.Checked;
        }

        /// <summary>
        /// Reading data from object to fields.
        /// </summary>
        private void ReadFromObject()
        {
            this.txtName.Text = rule.Name;
            if (rule.FromDateTime != null) {
                this.dtpFromDateAndTime.Checked = true;
                this.dtpFromDateAndTime.Value = rule.FromDateTime.Value;
            } else {
                this.dtpFromDateAndTime.Checked = false;
                this.dtpFromDateAndTime.Value = DateTime.Now;
            }
            if (rule.ToDateTime != null) {
                this.dtpToDateAndTime.Checked = true;
                this.dtpToDateAndTime.Value = rule.ToDateTime.Value;
            } else {
                this.dtpToDateAndTime.Checked = false;
                this.dtpToDateAndTime.Value = DateTime.Now;
            }
            if (rule.DurationInMinutes != null) {
                this.nudDuration.Value = rule.DurationInMinutes.Value;
            } else {
                this.nudDuration.Value = 0;
            }
            if (rule.DayOfWeek != null) {
                for (int c = 0; c < this.cboDayOfWeek.Items.Count; c++)
                {
                    ParentControl.ObjectModel.DayOfWeek dow = (ParentControl.ObjectModel.DayOfWeek)this.cboDayOfWeek.Items[c];
                    if (dow.Id == rule.DayOfWeek)
                    {
                        cboDayOfWeek.SelectedIndex = c;
                        break;
                    }
                }
            } else {
                cboDayOfWeek.SelectedValue = null;
            }
            chkEnabled.Checked = rule.Enabled;
        }

        /// <summary>
        /// Click on OK button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtName.Text) || string.IsNullOrWhiteSpace(this.txtName.Text))
            {
                this.DialogResult = DialogResult.None;
                this.txtName.Focus();
                throw new WarningException(
                    ParentControlClient.Strings.GetString("field_is_required")
                        .Replace("%s", ParentControlClient.Strings.GetString("name")));
            }
            WriteFieldsToObject();
            RulesRemote rulesRemote = new RulesRemote();
            try {
                rulesRemote.StoreRule(rule);
            } catch (System.Exception ex) {
                this.DialogResult = DialogResult.None;
                throw ex;
            }
        }
    }

}
