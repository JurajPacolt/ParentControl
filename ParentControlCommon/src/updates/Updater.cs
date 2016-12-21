/* Created on 9.9.2016 */
using log4net;
using ParentControlCommon.Updates.Base;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Text;

namespace ParentControlCommon.Updates
{
    /// <summary>
    /// Updater for database updates.
    /// </summary>
    public class Updater
    {
        protected ILog logger = LogManager.GetLogger(typeof(Updater));

        public const String NAMESPACE_PREFIX = "UpdateSeria_";
        public const String UPDATE_CLASS_PREFIX = "Update_";

        /// <summary>
        /// Method checking if is needed database to update.
        /// </summary>
        public void TryToUpdate()
        {
            logger.Info("Trying to auto update database ...");

            // We have to prepare dictionary with all needed types.
            Dictionary<string, Type> updateClasses = new Dictionary<string, Type>();
            int idx;
            String namespaceName;
            Assembly asm = Assembly.GetEntryAssembly();
            foreach (Type type in asm.GetTypes())
            {
                idx = type.Namespace.LastIndexOf(".");
                if (idx >= 0)
                {
                    namespaceName = type.Namespace.Substring(idx + 1);
                    if (namespaceName.StartsWith(NAMESPACE_PREFIX) 
                        && namespaceName.IndexOf(".") < 0 
                        && type.Name.StartsWith(UPDATE_CLASS_PREFIX))
                    {
                        updateClasses.Add(namespaceName + "|" + type.Name, type);
                    }
                }
            }

            DataVersion actualDataVersion = GetDataVersionCreateIfNeeded();
            logger.Info("Actual data version " + actualDataVersion.ToString());

            int major = actualDataVersion.Major;
            int minor = actualDataVersion.Minor;
            String key;
            bool wasUpdate = false;
            Type updateType;
            AbstractUpdate update;
            for (;;)
            {
                minor++;
                key = GenerateKey(major, minor);
                if (!updateClasses.ContainsKey(key)) {
                    break;
                }
                for (;;)
                {
                    key = GenerateKey(major, minor);
                    if (!updateClasses.ContainsKey(key)) {
                        minor = 0;
                        break;
                    }
                    logger.Info("Trying update to " + major + "." + minor);
                    using (DbTransaction tra = DataAccess.Instance.Connection.BeginTransaction())
                    {
                        updateType = updateClasses[key];
                        update = (AbstractUpdate)asm.CreateInstance(updateType.Namespace + "." + updateType.Name);
                        update.Update();

                        UpdateDataVersion(major, minor);

                        tra.Commit();
                        logger.Info("Updated to " + major + "." + minor + " successful.");
                    }
                    wasUpdate = true;
                    minor++;
                }
                major++;
            }

            if (!wasUpdate) {
                logger.Info("Nothing to update ...");
            }

            logger.Info("Auto update database ... DONE");
        }

        /// <summary>
        /// Updateing data version.
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="major"></param>
        /// <param name="minor"></param>
        private void UpdateDataVersion(int major, int minor)
        {
            using (DbCommand cmd = DataAccess.Instance.Connection.CreateCommand())
            {
                cmd.CommandText = "update DATA_VERSION set MAJOR=@major, MINOR=@minor";

                DbParameter param1 =  cmd.CreateParameter();
                param1.ParameterName = "major";
                param1.Value = major;
                cmd.Parameters.Add(param1);

                DbParameter param2 = cmd.CreateParameter();
                param2.ParameterName = "minor";
                param2.Value = minor;
                cmd.Parameters.Add(param2);

                cmd.Prepare();
                int rows = cmd.ExecuteNonQuery();
                if (rows != 1)
                {
                    throw new InvalidOperationException("Wrong updateing of DATA_VERSION. > " + rows);
                }
            }
        }

        /// <summary>
        /// Getting data version. If table doesn't exists will be created.
        /// </summary>
        private DataVersion GetDataVersionCreateIfNeeded()
        {
            DataVersion dataVersion = null;

            using (DbTransaction tra = DataAccess.Instance.Connection.BeginTransaction())
            {

                using (DbCommand cmd = DataAccess.Instance.Connection.CreateCommand())
                {
                    cmd.CommandText = "create table if not exists DATA_VERSION (MAJOR int, MINOR int)";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "select MAJOR, MINOR from DATA_VERSION";
                    using (DbDataReader reader = cmd.ExecuteReader()) {
                        if (reader.Read()) {
                            dataVersion = new DataVersion();
                            dataVersion.Major = reader.GetInt32(reader.GetOrdinal("MAJOR"));
                            dataVersion.Minor = reader.GetInt32(reader.GetOrdinal("MINOR"));
                        }
                        reader.Close();
                    }

                    if (dataVersion == null) {
                        cmd.CommandText = "insert into DATA_VERSION (MAJOR, MINOR) values (1, 0)";
                        cmd.ExecuteNonQuery();
                        dataVersion = new DataVersion();
                        dataVersion.Major = 1;
                        dataVersion.Minor = 0;
                    }
                }
                tra.Commit();
            }
            return dataVersion;
        }

        /// <summary>
        /// Helper method for generating key for internal dictionary.
        /// </summary>
        /// <param name="major"></param>
        /// <param name="minor"></param>
        /// <returns></returns>
        private String GenerateKey(int major, int minor)
        {
            return NAMESPACE_PREFIX + String.Format("{0:0000}", major) + "|" 
                + UPDATE_CLASS_PREFIX + String.Format("{0:00000}", minor);
        }

        /// <summary>
        /// DataVersion structure;
        /// </summary>
        private class DataVersion
        {
            private int major = 0;
            private int minor = 0;

            public int Major {
                get {
                    return major;
                }
                set {
                    this.major = value;
                }
            }
            public int Minor {
                get {
                    return minor;
                }
                set {
                    this.minor = value;
                }
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(major).Append(".");
                sb.Append(minor);
                return sb.ToString();
            }
        }

    }

}
