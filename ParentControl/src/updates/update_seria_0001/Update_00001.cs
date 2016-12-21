/* Created on 9.9.2016 */
using ParentControlCommon.Updates.Base;
using System.Text;

namespace ParentControl.Updates.UpdateSeria_0001
{
    class Update_00001 : AbstractUpdate
    {
        public override void Update()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("create table RULE (");
            sql.Append("    ID integer primary key autoincrement, ");
            sql.Append("    NAME text null, ");
            sql.Append("    DAY_OF_WEEK integer null, ");
            sql.Append("    DURATION integer null, ");
            sql.Append("    FROM_DATE_TIME datetime null, ");
            sql.Append("    TO_DATE_TIME datetime null, ");
            sql.Append("    ENABLED boolean null");
            sql.Append(")");
            ExecuteUpdate(sql.ToString());

            sql.Clear();
            sql.Append("create table OBSERVED_VALUES (");
            sql.Append("    ACTUAL_DATE date null, ");
            sql.Append("    DURATION long null ");
            sql.Append(")");
            ExecuteUpdate(sql.ToString());

            sql.Clear();
            sql.Append("insert into OBSERVED_VALUES (ACTUAL_DATE, DURATION) values (null, null)");
            ExecuteUpdate(sql.ToString());

            sql.Clear();
            sql.Append("create table BASE_SETTINGS (");
            sql.Append("    SHUTDOWN_COMMAND text null, ");
            sql.Append("    CHECK_INTERVAL integer null, ");
            sql.Append("    DELAY_START integer null, ");
            sql.Append("    SERVICE_URL text null ");
            sql.Append(")");
            ExecuteUpdate(sql.ToString());

            sql.Clear();
            sql.Append("insert into BASE_SETTINGS (SHUTDOWN_COMMAND, CHECK_INTERVAL, DELAY_START, SERVICE_URL) values ('shutdown /f /s', 60, null, null)");
            ExecuteUpdate(sql.ToString());
        }

    }
}
