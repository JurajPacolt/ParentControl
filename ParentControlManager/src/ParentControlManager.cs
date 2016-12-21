/* Created on 22.10.2016 */
using log4net;
using ParentControlCommon;
using ParentControlCommon.Updates;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace ParentControlManager
{
    /// <summary>
    /// Entry class.
    /// </summary>
    public class ParentControlManager
    {
        private static ParentControlManager instance;
        private static ResourceManager stringsResources;

        public static bool IsLogedAdministrator => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

        protected ILog logger = LogManager.GetLogger(typeof(ParentControlManager));

        private MainForm mainForm;

        public const string DATABASE_FILE = "ParentControlManager.db";

        /// <summary>
        /// Default constructor.
        /// </summary>
        private ParentControlManager()
        {
            logger.Info("Starting up ...");

            logger.Info("Initialize database ...");
            DataAccess.Instance.Init(DATABASE_FILE);

            new Updater().TryToUpdate();

            logger.Info("Create instance of the main form.");
            mainForm = new MainForm();
            mainForm.Disposed += new EventHandler(MainForm_Disposed);
        }

        /// <summary>
        /// Main form - disposed event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MainForm_Disposed(object sender, EventArgs args)
        {
            mainForm.Disposed -= MainForm_Disposed;
            mainForm = null;
        }

        /// <summary>
        /// Exit application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnExit(object sender, EventArgs e)
        {
            DataAccess.Instance.Close();
            Application.Exit();
        }

        /// <summary>
        /// Getter of the main form.
        /// </summary>
        public MainForm MainForm
        {
            get
            {
                return mainForm;
            }
        }

        /// <summary>
        /// Strings resources.
        /// </summary>
        public static ResourceManager Strings
        {
            get
            {
                if (stringsResources == null)
                {

                    stringsResources =
                        new ResourceManager("ParentControlManager.Properties.Strings",
                        typeof(ParentControlManager).Assembly);

                    // Via title we can test culture.
                    String title = null;
                    try
                    {
                        title = Strings.GetString("application_title", CultureInfo.CurrentCulture);
                    }
                    catch (MissingManifestResourceException)
                    {
                        log4net.LogManager.GetLogger(typeof(ParentControlManager)).Warn(
                            "Could not found resources for '" + CultureInfo.CurrentCulture.Name + "'.");
                    }
                    if (title == null)
                    {
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                    }
                }
                return stringsResources;
            }
        }

        /// <summary>
        /// Getter for main client object.
        /// </summary>
        public static ParentControlManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ParentControlManager();
                }
                return instance;
            }
        }

        /// <summary>
        /// Global application exit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            ParentControlManager.Instance.OnExit(null, null);
        }

        /// <summary>
        /// Exception handling.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            if (typeof(WarningException).IsInstanceOfType(e.Exception))
            {
                log4net.LogManager.GetLogger(e.Exception.TargetSite.ReflectedType).Warn(e.Exception.Message, e.Exception);
                MessageBox.Show(e.Exception.Message, Strings.GetString("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                log4net.LogManager.GetLogger(e.Exception.TargetSite.ReflectedType).Error(e.Exception.Message, e.Exception);
                MessageBox.Show(e.Exception.Message, Strings.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.ThreadException += Application_ThreadException;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.ApplicationExit += Application_ApplicationExit;
                Application.Run(ParentControlManager.Instance.MainForm);
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(typeof(ParentControlManager)).Error(ex.Message, ex);
                MessageBox.Show(ex.Message, Strings.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
