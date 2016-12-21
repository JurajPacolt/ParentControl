/* Created on 9.9.2016 */
using System;
using System.Data.Common;

namespace ParentControlCommon.Updates.Base
{
    /// <summary>
    /// Abstract class of the updates.
    /// </summary>
    public abstract class AbstractUpdate
    {
        protected DbConnection conn;

        public AbstractUpdate()
        {
            this.conn = DataAccess.Instance.Connection;
        }

        /// <summary>
        /// Calling by updater.
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Helpful method for executing update.
        /// </summary>
        /// <returns></returns>
        public int ExecuteUpdate(String sql)
        {
            using (DbCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                return cmd.ExecuteNonQuery();
            }
        }

    }

}
