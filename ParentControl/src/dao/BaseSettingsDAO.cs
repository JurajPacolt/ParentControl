/* Created on 8.10.2016 */
using ParentControl.ObjectModel;
using ParentControlCommon;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Text;
using static ParentControl.DataModel;

namespace ParentControl.DAO
{
    /// <summary>
    /// DAO for base settings.
    /// </summary>
    public class BaseSettingsDAO
    {
        /// <summary>
        /// Getter for base settings.
        /// </summary>
        /// <returns></returns>
        public BaseSettings GetBaseSettings()
        {
            using (SQLiteCommand cmd = (SQLiteCommand)DataAccess.Instance.Connection.CreateCommand())
            {
                cmd.CommandText = "select * from " + Tables.BASE_SETTINGS;
                using (DbDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        BaseSettings bs = new BaseSettings();
                        bs.ShutdownCommand = 
                            !rd.IsDBNull(rd.GetOrdinal(Columns.BASE_SETTINGS.SHUTDOWN_COMMAND)) 
                            ? rd.GetString(rd.GetOrdinal(Columns.BASE_SETTINGS.SHUTDOWN_COMMAND)) 
                            : null;
                        bs.CheckInterval = 
                            !rd.IsDBNull(rd.GetOrdinal(Columns.BASE_SETTINGS.CHECK_INTERVAL)) 
                            ? (int?)rd.GetInt32(rd.GetOrdinal(Columns.BASE_SETTINGS.CHECK_INTERVAL)) 
                            : null;
                        bs.UrlService = 
                            !rd.IsDBNull(rd.GetOrdinal(Columns.BASE_SETTINGS.SERVICE_URL)) 
                            ? rd.GetString(rd.GetOrdinal(Columns.BASE_SETTINGS.SERVICE_URL)) 
                            : null;
                        bs.DelayStart =
                            !rd.IsDBNull(rd.GetOrdinal(Columns.BASE_SETTINGS.DELAY_START))
                            ? (int?)rd.GetInt32(rd.GetOrdinal(Columns.BASE_SETTINGS.DELAY_START))
                            : null;
                        return bs;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Storing base settings.
        /// </summary>
        /// <param name="bs"></param>
        public void StoreBaseSettings(BaseSettings bs)
        {
            BaseSettings bsOld = GetBaseSettings();

            using (DbTransaction tra = DataAccess.Instance.Connection.BeginTransaction())
            {
                using (SQLiteCommand cmd = (SQLiteCommand)DataAccess.Instance.Connection.CreateCommand())
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("update ").Append(Tables.BASE_SETTINGS).Append(" ");
                    sql.Append("set ").Append(Columns.BASE_SETTINGS.SHUTDOWN_COMMAND).Append(" = @shutdownCommand, ");
                    sql.Append(Columns.BASE_SETTINGS.CHECK_INTERVAL).Append(" = @checkInterval, ");
                    sql.Append(Columns.BASE_SETTINGS.SERVICE_URL).Append(" = @serviceUrl, ");
                    sql.Append(Columns.BASE_SETTINGS.DELAY_START).Append(" = @delayStart");

                    cmd.CommandText = sql.ToString();

                    cmd.Parameters.Add("shutdownCommand", DbType.String).Value = bs.ShutdownCommand;
                    cmd.Parameters.Add("checkInterval", DbType.Int32).Value = bs.CheckInterval;
                    cmd.Parameters.Add("serviceUrl", DbType.String).Value = bs.UrlService;
                    cmd.Parameters.Add("delayStart", DbType.Int32).Value = bs.DelayStart;

                    int rows = cmd.ExecuteNonQuery();
                    if (rows == 0)
                    {
                        throw new InvalidOperationException("Could not update BASE SETTINGS.");
                    }
                    else if (rows > 1)
                    {
                        throw new InvalidOperationException("Too many rows updated in BASE SETTINGS.");
                    }
                }
                tra.Commit();

                ParentControlFactory.Instance.RestartScheduler();
            }
        }
    }

}
