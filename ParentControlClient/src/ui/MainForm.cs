/* Created on 18.9.2016 */
using ParentControl.ObjectModel;
using ParentControlClient.Remote;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;

namespace ParentControlClient.UI
{
    /// <summary>
    /// Main form.
    /// </summary>
    public partial class MainForm : Form
    {

        private RulesRemote rulesRemote = new RulesRemote();

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            // Form title.
            this.Text = ParentControlClient.Strings.GetString("application_title");
            this.Icon = Icon.FromHandle(Properties.Resources.app_icon.GetHicon());

            // Texts for column's headers.
            this.dataGridViewRules.Columns[0].HeaderText = ParentControlClient.Strings.GetString("name");
            this.dataGridViewRules.Columns[1].HeaderText = ParentControlClient.Strings.GetString("from");
            this.dataGridViewRules.Columns[2].HeaderText = ParentControlClient.Strings.GetString("to");
            this.dataGridViewRules.Columns[3].HeaderText = ParentControlClient.Strings.GetString("duration");
            this.dataGridViewRules.Columns[4].HeaderText = ParentControlClient.Strings.GetString("day_of_week");
            this.dataGridViewRules.Columns[5].HeaderText = ParentControlClient.Strings.GetString("enabled");

            // Aligments for data grid headers.
            this.dataGridViewRules.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewRules.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewRules.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewRules.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewRules.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewRules.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.btnNewRule.Text = ParentControlClient.Strings.GetString("new_rule");
            this.btnNewRule.ToolTipText = ParentControlClient.Strings.GetString("new_rule");
            this.btnModifyRule.Text = ParentControlClient.Strings.GetString("change_rule");
            this.btnModifyRule.ToolTipText = ParentControlClient.Strings.GetString("change_rule");
            this.btnDeleteRule.Text = ParentControlClient.Strings.GetString("delete_rule");
            this.btnDeleteRule.ToolTipText = ParentControlClient.Strings.GetString("delete_rule");
            this.btnSettings.Text = ParentControlClient.Strings.GetString("settings");
            this.btnSettings.ToolTipText = ParentControlClient.Strings.GetString("settings");

            // Privilegies
            /*bool admin = ParentControlClient.IsLogedAdministrator;

            this.btnNewRule.Enabled = admin;
            this.btnModifyRule.Enabled = admin;
            this.btnDeleteRule.Enabled = admin;
            this.btnSettings.Enabled = admin;

            if (admin) {
                this.dataGridViewRules.DoubleClick += DataGridViewRules_DoubleClick;
            }*/
        }

        /// <summary>
        /// Double click on grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewRules_DoubleClick(object sender, EventArgs e)
        {
            if (this.dataGridViewRules.SelectedRows.Count > 0) {
                ModifyRuleRecord();
            }
        }

        /// <summary>
        /// On load form.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            // Form will be hidden on load.
            base.OnLoad(e);

            // On load refresh grid.
            RefreshData();
        }

        /// <summary>
        /// Refresh data on grid.
        /// </summary>
        public void RefreshData()
        {
            Rule[] rules = rulesRemote.ListRules();

            dataGridViewRules.Rows.Clear();

            foreach (Rule rule in rules)
            {
                dataGridViewRules.Rows.Add(RuleObjectToDataGridViewRow(rule));
            }
        }

        /// <summary>
        /// Prepare row for grid from rule object.
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        public DataGridViewRow RuleObjectToDataGridViewRow(Rule rule)
        {
            DataGridViewRow row = (DataGridViewRow)dataGridViewRules.RowTemplate.Clone();
            row.Tag = rule;

            object[] objs = new object[6];
            objs[0] = rule.Name;
            objs[1] = (rule.FromDateTime != null ? rule.FromDateTime.Value.ToString("dd.MM.yyyy") : "");
            objs[2] = (rule.ToDateTime != null ? rule.ToDateTime.Value.ToString("dd.MM.yyyy") : "");
            objs[3] = (rule.DurationInMinutes != null ? rule.DurationInMinutes.Value.ToString("#") : "");
            objs[4] = rule.DayOfWeek != null ? GetDayOfWeek(rule.DayOfWeek.Value) : "";
            objs[5] = rule.Enabled;
            row.CreateCells(dataGridViewRules, objs);

            row.Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            row.Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            row.Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            row.Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            row.Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            row.Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            return row;
        }

        /// <summary>
        /// Getting day of week in string.
        /// </summary>
        /// <param name="iDayOfWeek"></param>
        /// <returns></returns>
        public static string GetDayOfWeek(int iDayOfWeek)
        {
            switch (iDayOfWeek)
            {
                case (int)System.DayOfWeek.Sunday:
                    return ParentControlClient.Strings.GetString("sunday");
                case (int)System.DayOfWeek.Monday:
                    return ParentControlClient.Strings.GetString("monday");
                case (int)System.DayOfWeek.Tuesday:
                    return ParentControlClient.Strings.GetString("tuesday");
                case (int)System.DayOfWeek.Wednesday:
                    return ParentControlClient.Strings.GetString("wednesday");
                case (int)System.DayOfWeek.Thursday:
                    return ParentControlClient.Strings.GetString("thursday");
                case (int)System.DayOfWeek.Friday:
                    return ParentControlClient.Strings.GetString("friday");
                case (int)System.DayOfWeek.Saturday:
                    return ParentControlClient.Strings.GetString("saturday");
                default:
                    return "<UNKNOWN>";
            }
        }

        /// <summary>
        /// Click on new rule button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewRule_Click(object sender, EventArgs e)
        {
            Rule rule = new Rule();
            RuleDetailForm detailForm = new RuleDetailForm(rule);
            detailForm.Text = ParentControlClient.Strings.GetString("new_rule");
            if (detailForm.ShowDialog(this) == DialogResult.OK)
            {
                RefreshData();

                if (this.dataGridViewRules.Rows.Count > 0)
                {
                    this.dataGridViewRules.Rows[this.dataGridViewRules.Rows.Count-1].Selected = true;
                }
            }
        }

        /// <summary>
        /// Request for editing of the rule record.
        /// </summary>
        private void ModifyRuleRecord()
        {
            if (this.dataGridViewRules.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    this, ParentControlClient.Strings.GetString("must_select_record"),
                    ParentControlClient.Strings.GetString("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedIndex = this.dataGridViewRules.SelectedRows[0].Index;

            Rule rule = (Rule)this.dataGridViewRules.SelectedRows[0].Tag;
            // We take a new instance from server, may be something was changed.
            rule = rulesRemote.GetRule(rule.Id.Value);

            RuleDetailForm detailForm = new RuleDetailForm(rule);
            detailForm.Text = ParentControlClient.Strings.GetString("change_rule");
            if (detailForm.ShowDialog(this) == DialogResult.OK)
            {
                RefreshData();

                if (selectedIndex >= 0 && selectedIndex < this.dataGridViewRules.Rows.Count)
                {
                    this.dataGridViewRules.Rows[selectedIndex].Selected = true;
                }
            }
        }

        /// <summary>
        /// Click on button for modify rule record.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModifyRule_Click(object sender, EventArgs e)
        {
            ModifyRuleRecord();
        }

        /// <summary>
        /// Click on delete rule.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteRule_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewRules.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    this, ParentControlClient.Strings.GetString("must_select_record"), 
                    ParentControlClient.Strings.GetString("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show(
                this, ParentControlClient.Strings.GetString("really_delete_record"), 
                ParentControlClient.Strings.GetString("question"), MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                int selectedIndex = this.dataGridViewRules.SelectedRows[0].Index;
                if (selectedIndex >= 0)
                {
                    Rule rule = (Rule)this.dataGridViewRules.SelectedRows[0].Tag;

                    rulesRemote.DeleteRule(rule.Id.Value);

                    RefreshData();

                    if (selectedIndex >= 0 && selectedIndex < this.dataGridViewRules.Rows.Count) {
                        this.dataGridViewRules.Rows[selectedIndex].Selected = true;
                    } else if (this.dataGridViewRules.Rows.Count > 0 && selectedIndex >= this.dataGridViewRules.Rows.Count) {
                        this.dataGridViewRules.Rows[this.dataGridViewRules.Rows.Count - 1].Selected = true;
                    }

                    if (this.dataGridViewRules.SelectedRows.Count > 0 && this.dataGridViewRules.SelectedRows[0].Index >= 0) {
                        this.dataGridViewRules.FirstDisplayedScrollingRowIndex = this.dataGridViewRules.SelectedRows[0].Index;
                    }
                }
            }
        }

        /// <summary>
        /// Opening main settings via button "Settings".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSettings_Click(object sender, EventArgs e)
        {
            BaseSettingsRemote remote = new BaseSettingsRemote();
            BaseSettings baseSettings = remote.GetBaseSettings();

            SettingsForm form = new SettingsForm(baseSettings);
            form.ShowDialog(this);
        }

        /// <summary>
        /// Button - reset duration.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(ParentControlClient.Strings.GetString("question_really_to_reset"), 
                ParentControlClient.Strings.GetString("question"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, 
                MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                ObservedValuesRemote remote = new ObservedValuesRemote();
                remote.Reset();
            }
        }

        /// <summary>
        /// Button - show information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInfo_Click(object sender, EventArgs e)
        {
            ObservedValuesRemote remote = new ObservedValuesRemote();
            ObservedValues ov = remote.GetObservedValues();

            String actualDate =
                (ov.ActualDate != null
                ? ov.ActualDate.Value.ToString(Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern)
                : "");
            String duration = "";
            if (ov.Duration != null)
            {
                TimeSpan t = TimeSpan.FromMilliseconds(ov.Duration.Value);
                string time = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms", t.Hours, t.Minutes, t.Seconds, t.Milliseconds);
                duration = time;
            }

            MessageBox.Show(actualDate + "\n" + duration, ParentControlClient.Strings.GetString("information"));
        }
    }

}
