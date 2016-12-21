/* Created on 2.10.2016 */
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
    /// DAO for observed values.
    /// </summary>
    public class ObservedValuesDAO
    {

        /// <summary>
        /// Getting observed values.
        /// </summary>
        /// <returns></returns>
        public ObservedValues GetObservedValues()
        {
            using (SQLiteCommand cmd = (SQLiteCommand)DataAccess.Instance.Connection.CreateCommand())
            {
                cmd.CommandText = "select * from " + Tables.OBSERVED_VALUES;
                using (DbDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        ObservedValues ov = new ObservedValues();
                        ov.ActualDate = 
                            !rd.IsDBNull(rd.GetOrdinal(Columns.OBSERVED_VALUES.ACTUAL_DATE)) 
                            ? (DateTime?)rd.GetDateTime(rd.GetOrdinal(Columns.OBSERVED_VALUES.ACTUAL_DATE)) 
                            : null;
                        ov.Duration = 
                            !rd.IsDBNull(rd.GetOrdinal(Columns.OBSERVED_VALUES.DURATION)) 
                            ? (long?)rd.GetInt64(rd.GetOrdinal(Columns.OBSERVED_VALUES.DURATION)) 
                            : null;
                        return ov;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Storing observed values.
        /// </summary>
        /// <param name="ov"></param>
        public void StoreObservedValues(ObservedValues ov)
        {
            using (DbTransaction tra = DataAccess.Instance.Connection.BeginTransaction())
            {
                using (SQLiteCommand cmd = (SQLiteCommand)DataAccess.Instance.Connection.CreateCommand())
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("update ").Append(Tables.OBSERVED_VALUES).Append(" ");
                    sql.Append("set ").Append(Columns.OBSERVED_VALUES.ACTUAL_DATE).Append(" = @actualDate, ");
                    sql.Append(Columns.OBSERVED_VALUES.DURATION).Append(" = @duration");

                    cmd.CommandText = sql.ToString();

                    cmd.Parameters.Add("actualDate", DbType.Date).Value = ov.ActualDate;
                    cmd.Parameters.Add("duration", DbType.Int64).Value = ov.Duration;

                    int rows = cmd.ExecuteNonQuery();
                    if (rows == 0)
                    {
                        throw new InvalidOperationException("Could not update OBSERVED VALUES.");
                    }
                    else if (rows > 1)
                    {
                        throw new InvalidOperationException("Too many rows updated in OBSERVED VALUES.");
                    }
                }
                tra.Commit();
            }
        }

    }
}
