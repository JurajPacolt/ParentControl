using log4net;
using System;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace ParentControlCommon
{
    /// <summary>
    /// Data access support.
    /// </summary>
    public class DataAccess
    {
        protected ILog logger = LogManager.GetLogger(typeof(DataAccess));

        /// <summary>
        /// Random generated password in constrant.
        /// </summary>
        public const String PASSWORD = @"wjvrj5h4KdeF4Hy8";

        /// <summary>
        /// Static instance of the data access.
        /// </summary>
        private static DataAccess instance = null;

        /// <summary>
        /// Database connection.
        /// </summary>
        private SQLiteConnection conn;

        /// <summary>
        /// Database file name;
        /// </summary>
        private string databaseFileName;

        /// <summary>
        /// Default constructor.
        /// </summary>
        private DataAccess()
        {
        }

        /// <summary>
        /// Set user permitions for database file.
        /// </summary>
        private void SetPermitionsForDbFile() {
            NTAccount ac = new NTAccount(@"Users");
            FileSecurity fs = File.GetAccessControl(DatabaseFile);
            fs.SetAccessRule(new FileSystemAccessRule(ac, FileSystemRights.FullControl, AccessControlType.Allow));
            File.SetAccessControl(DatabaseFile, fs);
        }

        /// <summary>
        /// Getter for database file with full path.
        /// </summary>
        public String DatabaseFile
        {
            get
            {
                return ParentControlCommon.Utils.DatabaseFolder + @"\" + databaseFileName;
            }
        }

        /// <summary>
        /// Getter for connection string.
        /// </summary>
        public String ConnectionString {
            get {
                return @"Data Source=" + DatabaseFile + "; Version=3; New=False; Compress=True; Password=" + PASSWORD + ";";
            }
        }

        /// <summary>
        /// Getter for database connection.
        /// </summary>
        public DbConnection Connection {
            get {
                return conn;
            }
        }

        public void Init(string dbFileName)
        {
            logger.Info("Internal database initialization.");

            this.databaseFileName = dbFileName;

            if (!Directory.Exists(Utils.DatabaseFolder))
            {
                Directory.CreateDirectory(Utils.DatabaseFolder);
            }

            if (!File.Exists(DatabaseFile))
            {
                SQLiteConnection.CreateFile(DatabaseFile);
                SetPermitionsForDbFile();
            }

            conn = new SQLiteConnection(ConnectionString);
            conn.Open();
        }

        public void Close()
        {
            logger.Info("Closing database connection.");
            conn.Close();
        }

        /// <summary>
        /// Getting static instance for data access.
        /// </summary>
        public static DataAccess Instance {
            get {
                if (instance == null)
                {
                    instance = new DataAccess();
                }
                return instance;
            }
        }

    }
}
