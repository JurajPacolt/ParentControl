/* Created on 27.10.2016 */
using ParentControlCommon;
using ParentControlManager.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Text;
using static ParentControlManager.DataModel;

namespace ParentControlManager.Provider
{
    /// <summary>
    /// Data provider for remote connections to the stations.
    /// </summary>
    public class ConnectionDataProvider
    {
        /// <summary>
        /// Getting connection list.
        /// </summary>
        /// <returns></returns>
        public IList<Connection> GetConnectionList()
        {
            IList<Connection> result = new List<Connection>();

            using (DbCommand cmd = DataAccess.Instance.Connection.CreateCommand())
            {
                cmd.CommandText = "select * from " + Tables.CONNECTION + " order by " + Columns.CONNECTION.NAME;
                using (DbDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        result.Add(ReadConnectionFromDataReader(rd));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Getting connection object.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Connection GetConnection(int id)
        {
            using (SQLiteCommand cmd = (SQLiteCommand)DataAccess.Instance.Connection.CreateCommand())
            {
                cmd.CommandText = "select * from " + Tables.CONNECTION + " where " + Columns.CONNECTION.ID + " = @id";
                cmd.Parameters.Add("id", DbType.Int32).Value = id;
                using (DbDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        return ReadConnectionFromDataReader(rd);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Read "Connection" object from DbDataReader.
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        private Connection ReadConnectionFromDataReader(DbDataReader rd)
        {
            Connection conn = new Connection();
            conn.Id = rd.GetInt32(rd.GetOrdinal(Columns.CONNECTION.ID));
            conn.Name = rd.GetString(rd.GetOrdinal(Columns.CONNECTION.NAME));
            conn.Hostname = rd.GetString(rd.GetOrdinal(Columns.CONNECTION.HOSTNAME));
            conn.Port = rd.GetInt32(rd.GetOrdinal(Columns.CONNECTION.PORT));
            return conn;
        }

        /// <summary>
        /// Storing connection model object to database.
        /// </summary>
        public void StoreConnection(Connection connection)
        {
            using (DbTransaction tra = DataAccess.Instance.Connection.BeginTransaction())
            {
                using (SQLiteCommand cmd = (SQLiteCommand)DataAccess.Instance.Connection.CreateCommand())
                {
                    cmd.Parameters.Add("name", DbType.String).Value = connection.Name;
                    cmd.Parameters.Add("hostname", DbType.String).Value = connection.Hostname;
                    cmd.Parameters.Add("port", DbType.Int32).Value = connection.Port;

                    StringBuilder sql = new StringBuilder();

                    if (connection.Id == null)
                    {
                        sql.Append("insert into ").Append(Tables.CONNECTION).Append(" (");
                        sql.Append(Columns.CONNECTION.NAME).Append(", ");
                        sql.Append(Columns.CONNECTION.HOSTNAME).Append(", ");
                        sql.Append(Columns.CONNECTION.PORT).Append(") values (");
                        sql.Append("@name, @hostname, @port)");
                    }
                    else
                    {
                        sql.Append("update ").Append(Tables.CONNECTION).Append(" set ");
                        sql.Append(Columns.CONNECTION.NAME).Append(" = @name, ");
                        sql.Append(Columns.CONNECTION.HOSTNAME).Append(" = @hostname, ");
                        sql.Append(Columns.CONNECTION.PORT).Append(" = @port ");
                        sql.Append("where ").Append(Columns.CONNECTION.ID).Append(" = @id");

                        cmd.Parameters.Add("id", DbType.Int32).Value = connection.Id;
                    }

                    cmd.CommandText = sql.ToString();
                    cmd.ExecuteNonQuery();

                    if (connection.Id == null)
                    {
                        cmd.CommandText = "SELECT last_insert_rowid()";
                        using (DbDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                connection.Id = dr.GetInt32(0);
                            }
                        }
                    }
                }
                tra.Commit();
            }
        }

        /// <summary>
        /// Delete connection record.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteConnection(int id)
        {
            using (DbTransaction tra = DataAccess.Instance.Connection.BeginTransaction())
            {
                using (SQLiteCommand cmd = (SQLiteCommand)DataAccess.Instance.Connection.CreateCommand())
                {
                    cmd.CommandText = "delete from " + Tables.CONNECTION + " where " + Columns.CONNECTION.ID + " = @id";
                    cmd.Parameters.Add("id", DbType.Int32).Value = id;
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        throw new InvalidOperationException("Could not found record with ID=" + id + ".");
                    }
                }
                tra.Commit();
            }
        }

    }

}
