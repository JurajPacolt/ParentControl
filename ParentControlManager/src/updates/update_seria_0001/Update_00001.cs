/* Created on 22.10.2016 */
using System;
using ParentControlCommon.Updates.Base;
using System.Text;

namespace ParentControlManager.Updates.UpdateSeria_0001
{
    public class Update_00001 : AbstractUpdate
    {
        public override void Update()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("create table CONNECTION (");
            sql.Append("  ID integer primary key autoincrement, ");
            sql.Append("  NAME text null, ");
            sql.Append("  HOSTNAME text null, ");
            sql.Append("  PORT integer null ");
            sql.Append(")");
            ExecuteUpdate(sql.ToString());
        }

    }
}
