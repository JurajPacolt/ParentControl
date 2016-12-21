/* Created on 22.9.2016 */
using System;
using System.Windows.Forms;
using ParentControlClient.UI;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.ComponentModel;
using System.Security.Principal;

namespace ParentControlClient
{
    public class ParentControlClient
    {
        private static ParentControlClient instance;
        private static ResourceManager stringsResources;

        public static bool IsLogedAdministrator => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

        private MainForm mainForm;
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;

        /// <summary>
        /// Default constructor.
        /// </summary>
        private ParentControlClient()
        {
            // Check and set culture info.
            String title = Strings.GetString("application_title");

            // Creating context menu for tray icon.
            trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add(new ToolStripSeparator());
            ToolStripMenuItem miExit = new ToolStripMenuItem(Strings.GetString("exit"), Properties.Resources.exit, OnExit);
            trayMenu.Items.Add(miExit);

            // Create instance of tray icon.
            trayIcon = new NotifyIcon();
            trayIcon.Text = title;
            trayIcon.Icon = new Icon(Icon.FromHandle(Properties.Resources.app_icon.GetHicon()), 40, 40);
            trayIcon.ContextMenuStrip = trayMenu;
            trayIcon.Visible = true;

            // Handler for double-click, opening/hiding main form.
            trayIcon.DoubleClick += new EventHandler(TrayIconDoubleClick);
        }

        /// <summary>
        /// If is double-click to tray icon.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void TrayIconDoubleClick(object sender, EventArgs args)
        {
            if (mainForm == null) {
                mainForm = new MainForm();
                mainForm.Disposed += new EventHandler(MainForm_Disposed);
            }
            mainForm.Visible = !mainForm.Visible;
            mainForm.BringToFront();
        }

        /// <summary>
        /// Main form - disposed event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MainForm_Disposed(object sender, EventArgs args) {
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
            trayIcon.Dispose();
            Application.Exit();
        }

        /// <summary>
        /// Getter of the main form.
        /// </summary>
        public MainForm MainForm {
            get {
                return mainForm;
            }
        }

        /// <summary>
        /// Strings resources.
        /// </summary>
        public static ResourceManager Strings {
            get {
                if (stringsResources == null) {

                    stringsResources = 
                        new ResourceManager("ParentControlClient.Properties.Strings", 
                        typeof(ParentControlClient).Assembly);

                    // Via title we can test culture.
                    String title = null;
                    try
                    {
                        title = Strings.GetString("application_title", CultureInfo.CurrentCulture);
                    }
                    catch (MissingManifestResourceException)
                    {
                        log4net.LogManager.GetLogger(typeof(ParentControlClient)).Warn(
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
        public static ParentControlClient Instance {
            get {
                if (instance == null) {
                    instance = new ParentControlClient();
                }
                return instance;
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try {
                Application.ThreadException += Application_ThreadException;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.ApplicationExit += Application_ApplicationExit;
                Application.Run(ParentControlClient.Instance.MainForm);
            } catch (Exception ex) {
                log4net.LogManager.GetLogger(typeof(ParentControlClient)).Error(ex.Message, ex);
                MessageBox.Show(ex.Message, Strings.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Global application exit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            ParentControlClient.Instance.OnExit(null, null);
        }

        /// <summary>
        /// Exception handling.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            if (typeof(WarningException).IsInstanceOfType(e.Exception)) {
                log4net.LogManager.GetLogger(e.Exception.TargetSite.ReflectedType).Warn(e.Exception.Message, e.Exception);
                MessageBox.Show(e.Exception.Message, Strings.GetString("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } else {
                log4net.LogManager.GetLogger(e.Exception.TargetSite.ReflectedType).Error(e.Exception.Message, e.Exception);
                MessageBox.Show(e.Exception.Message, Strings.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }

}
