/* Created on 2.10.2016 */
using ParentControlCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using static ParentControl.DataModel;

namespace ParentControl.DAO
{
    /// <summary>
    /// DAO for rules.
    /// </summary>
    public class RulesDAO
    {
        /// <summary>
        /// Storing rule, new or exists. If "id" field is NULL then is automatically new record.
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        public ObjectModel.Rule StoreRule(ObjectModel.Rule rule)
        {
            using (DbTransaction tra = DataAccess.Instance.Connection.BeginTransaction())
            {
                using (SQLiteCommand cmd = (SQLiteCommand)DataAccess.Instance.Connection.CreateCommand())
                {
                    StringBuilder sql = new StringBuilder();

                    if (rule.Id != null)
                    {
                        sql.Append("update ").Append(Tables.RULE).Append(" set ");
                        sql.Append(Columns.RULE.NAME).Append(" = @name, ");
                        sql.Append(Columns.RULE.DAY_OF_WEEK).Append(" = @dayOfWeek, ");
                        sql.Append(Columns.RULE.DURATION).Append(" = @duration, ");
                        sql.Append(Columns.RULE.FROM_DATE_TIME).Append(" = @from, ");
                        sql.Append(Columns.RULE.TO_DATE_TIME).Append(" = @to, ");
                        sql.Append(Columns.RULE.ENABLED).Append(" = @enabled ");
                        sql.Append("where ").Append(Columns.RULE.ID).Append(" = @id");
                    }
                    else
                    {
                        sql.Append("insert into ").Append(Tables.RULE).Append(" (");
                        sql.Append(Columns.RULE.NAME).Append(", ").Append(Columns.RULE.DAY_OF_WEEK).Append(", ");
                        sql.Append(Columns.RULE.DURATION).Append(", ").Append(Columns.RULE.FROM_DATE_TIME).Append(", ");
                        sql.Append(Columns.RULE.TO_DATE_TIME).Append(", ").Append(Columns.RULE.ENABLED);
                        sql.Append(") values (");
                        sql.Append("@name, @dayOfWeek, @duration, @from, @to, @enabled)");
                    }
                    cmd.CommandText = sql.ToString();

                    cmd.Parameters.Add("name", DbType.String).Value = rule.Name;
                    cmd.Parameters.Add("dayOfWeek", DbType.Int32).Value = rule.DayOfWeek;
                    cmd.Parameters.Add("duration", DbType.Int32).Value = rule.DurationInMinutes == 0 ? null : rule.DurationInMinutes;
                    cmd.Parameters.Add("from", DbType.DateTime).Value = rule.FromDateTime;
                    cmd.Parameters.Add("to", DbType.DateTime).Value = rule.ToDateTime;
                    cmd.Parameters.Add("enabled", DbType.Boolean).Value = rule.Enabled;
                    if (rule.Id != null)
                    {
                        cmd.Parameters.Add("id", DbType.Int32).Value = rule.Id;
                    }
                    cmd.ExecuteNonQuery();

                    if (rule.Id == null)
                    {
                        cmd.CommandText = "SELECT last_insert_rowid()";
                        using (DbDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                rule.Id = dr.GetInt32(0);
                            }
                        }
                    }
                }
                tra.Commit();
            }
            return rule;
        }

        /// <summary>
        /// Getting rule object via ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ObjectModel.Rule GetRule(int id)
        {
            using (SQLiteCommand cmd = (SQLiteCommand)DataAccess.Instance.Connection.CreateCommand())
            {
                cmd.CommandText = "select * from " + Tables.RULE + " where " + Columns.RULE.ID + " = @id";
                cmd.Parameters.Add("id", DbType.Int32).Value = id;
                using (DbDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        return readRuleFromDataReader(rd);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Delete rule via ID. If not exists then generated exception.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteRule(int id)
        {
            using (DbTransaction tra = DataAccess.Instance.Connection.BeginTransaction())
            {
                using (SQLiteCommand cmd = (SQLiteCommand)DataAccess.Instance.Connection.CreateCommand())
                {
                    cmd.CommandText = "delete from " + Tables.RULE + " where " + Columns.RULE.ID + " = @id";
                    cmd.Parameters.Add("id", DbType.Int32).Value = id;
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        throw new InvalidOperationException("Could not found record with ID=" + id + ".");
                    }
                }
                tra.Commit();
            }
        }

        /// <summary>
        /// Getting all exists rules.
        /// </summary>
        /// <returns></returns>
        public ObjectModel.Rule[] ListRules()
        {
            IList<ObjectModel.Rule> result = new List<ObjectModel.Rule>();

            using (DbCommand cmd = DataAccess.Instance.Connection.CreateCommand())
            {
                cmd.CommandText = "select * from " + Tables.RULE + " order by " + Columns.RULE.ID;
                using (DbDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        result.Add(readRuleFromDataReader(rd));
                    }
                }
            }
            return result.ToArray();
        }

        /// <summary>
        /// Read data from result set to object.
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private ObjectModel.Rule readRuleFromDataReader(DbDataReader rd)
        {
            ObjectModel.Rule rule = new ObjectModel.Rule();

            rule.Id = rd.GetInt32(rd.GetOrdinal(Columns.RULE.ID));
            rule.Name = 
                !rd.IsDBNull(rd.GetOrdinal(Columns.RULE.NAME)) 
                ? rd.GetString(rd.GetOrdinal(Columns.RULE.NAME)) 
                : null;
            rule.DayOfWeek = 
                !rd.IsDBNull(rd.GetOrdinal(Columns.RULE.DAY_OF_WEEK)) 
                ? (int?)rd.GetInt32(rd.GetOrdinal(Columns.RULE.DAY_OF_WEEK)) 
                : null;
            rule.DurationInMinutes = 
                !rd.IsDBNull(rd.GetOrdinal(Columns.RULE.DURATION)) 
                ? (int?)rd.GetInt32(rd.GetOrdinal(Columns.RULE.DURATION)) 
                : null;
            rule.Enabled = rd.GetBoolean(rd.GetOrdinal(Columns.RULE.ENABLED));
            rule.FromDateTime = 
                !rd.IsDBNull(rd.GetOrdinal(Columns.RULE.FROM_DATE_TIME)) 
                ? (DateTime?)rd.GetDateTime(rd.GetOrdinal(Columns.RULE.FROM_DATE_TIME)) 
                : null;
            rule.ToDateTime = 
                !rd.IsDBNull(rd.GetOrdinal(Columns.RULE.TO_DATE_TIME)) 
                ? (DateTime?)rd.GetDateTime(rd.GetOrdinal(Columns.RULE.TO_DATE_TIME)) 
                : null;

            return rule;
        }
    }

}
