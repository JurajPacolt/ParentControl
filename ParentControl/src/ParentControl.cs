using System;
using System.Collections;
using System.Configuration.Install;
using System.ServiceProcess;
using System.Threading;
using System.Net;
using log4net;
using System.Windows.Forms;

namespace ParentControl
{
    public class ParentControl : ServiceBase
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(ParentControl));

        public static String VERSION = "1.0";
        public static string MyServiceName = "ParentControl";
        public static string MyServiceDescription = "Parent Control Service";

        public const string DATABASE_FILE = "ParentControl.db";

        public ParentControl()
        {
            InitComponent();
        }

        private void InitComponent()
        {
            this.ServiceName = ParentControl.MyServiceName;
        }

        /// <summary>
        /// Start this service.
        /// </summary>
        protected override void OnStart(string[] args)
        {
            Thread.CurrentThread.Name = "Main";
            log.Info("Starting up service ...");
            ParentControlFactory.Instance.Startup();
        }

        /// <summary>
        /// Stop this service.
        /// </summary>
        protected override void OnStop()
        {
            log.Info("Shutting down service ...");
            ParentControlFactory.Instance.Shutdown();
        }

        /// <summary>
        /// Install service method.
        /// </summary>
        /// <param name="install"></param>
        private static void InstallService(bool install)
        {
            try
            {
                using (TransactedInstaller ti = new TransactedInstaller())
                {
                    using (ParentControlInstaller pi = new ParentControlInstaller())
                    {
                        ti.Installers.Add(pi);

                        string[] cmdline = {
                                string.Format(
                                    "/assemblypath={0}",
                                    System.Reflection.Assembly.GetExecutingAssembly().Location
                                )
                            };

                        pi.Context = new InstallContext(null, cmdline);

                        if (install)
                            pi.Install(new Hashtable());
                        else
                            pi.Uninstall(null);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Global exception handling.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        static void UnhandledExceptionEvent(object sender, UnhandledExceptionEventArgs args)
        {
            Exception ex = (Exception)args.ExceptionObject;
            log.Error(ex.Message, ex);
            if (ex.GetBaseException() != null) {
                log.Error("BASE-EXCEPTION: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Global application shutdown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void ShutdownHook(object sender, EventArgs e)
        {
            ParentControlFactory.Instance.Shutdown();
        }

        /// <summary>
        /// Entry point of the application.
        /// </summary>
        /// <param name="args">Program arguments.</param>
        static void Main(string[] args)
        {
            AppDomain dom = AppDomain.CurrentDomain;
            dom.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionEvent);
            dom.ProcessExit += new EventHandler(ShutdownHook);

            if (!HttpListener.IsSupported)
            {
                log.Error("Windows XP SP2 or Server 2003 is required to use.");
                return;
            }

            log.Info("Starting up ParentControl ...");

            if (args.Length == 0)
            {
                System.ServiceProcess.ServiceBase[] ServicesToRun;
                ServicesToRun = new
                    System.ServiceProcess.ServiceBase[] { new ParentControl() };
                System.ServiceProcess.ServiceBase.Run(ServicesToRun);
            }
            else if (args.Length >= 1)
            {
                for (int c = 0; c < args.Length; c++)
                {
                    String param = args[c];
                    if (param.Trim().StartsWith("--") || param.Trim().StartsWith("-"))
                    {
                        param = param.ToLower().Trim();
                    }

                    if (param.Equals("--help") || param.Equals("-h"))
                    {
                        String s1 = "\nParent Control v" + VERSION;
                        Console.WriteLine(s1);
                        for (int d = 0; d < s1.Length; d++) { Console.Write("="); }
                        Console.WriteLine();
                        Console.WriteLine("--help			-h	show this help");
                        Console.WriteLine("--start			-s	start up this service at the console");
                        Console.WriteLine("--service-name		-n	set service name");
                        Console.WriteLine("--service-description	-d	set service description");
                        Console.WriteLine("--install		-i	install service");
                        Console.WriteLine("--uninstall		-u	uninstall service");
                        break;
                    }
                    else if (param.Equals("--start") || param.Equals("-s"))
                    {
                        Thread.CurrentThread.Name = "Main";
                        ParentControlFactory.Instance.Startup();
                        Application.Run();
                        break;
                    }
                    else if (param.Equals("--install") || param.Equals("-i"))
                    {
                        InstallService(true);
                    }
                    else if (param.Equals("--uninstall") || param.Equals("-u"))
                    {
                        InstallService(false);
                    }
                    else if (param.Equals("--service-name") || param.Equals("-n"))
                    {
                        c++;
                        ParentControl.MyServiceName = args[c];
                    }
                    else if (param.Equals("--service-description") || param.Equals("-d"))
                    {
                        c++;
                        ParentControl.MyServiceDescription = args[c];
                    }
                }
            }
        }

    }

}
