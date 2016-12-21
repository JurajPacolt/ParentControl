using System;
using System.Xml;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;
using Microsoft.Deployment.WindowsInstaller;

namespace ParentControlInstallerCustomAction
{
    public class CustomActions
    {
        /// <summary>
        /// Private method for checking connection.
        /// </summary>
        /// <param name="hostname"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        private static bool CheckPort(string hostname, int port)
        {
            bool connectionOkay = false;
            try
            {
                using (TcpClient tc = new TcpClient())
                {
                    tc.Connect(hostname, port);
                    bool stat = tc.Connected;
                    if (stat)
                    {
                        connectionOkay = true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return connectionOkay;
        }

        /// <summary>
        /// Checking connection to ParentControl service.
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        [CustomAction]
        public static ActionResult CheckConnectionCustomAction(Session session)
        {
            session.Log("Begin - CheckConnectionCustomAction");

            String hostname = session["LISTENERHOST"];
            if (string.IsNullOrWhiteSpace(hostname))
            {
                hostname = "localhost";
            }
            session.Log("CheckConnectionCustomAction - hostname: " + hostname);
            String sPort = session["PORTNUMBER"];
            if (string.IsNullOrWhiteSpace(sPort))
            {
                sPort = "5556";
            }
            int port = Convert.ToInt32(sPort);
            session.Log("CheckConnectionCustomAction - port: " + port);

            if (CheckPort(hostname, port))
            {
                // TODO Doriesit lokalizaciu ...
                MessageBox.Show(session["TestConnectionSuccessfulString"], session["InformationString"], MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // TODO Doriesit lokalizaciu ...
                MessageBox.Show(session["TestConnectionFailedString"], session["WarningString"], MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            session.Log("End - CheckConnectionCustomAction");
            return ActionResult.Success;
        }

        /// <summary>
        /// Finding free port for listener.
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        [CustomAction]
        public static ActionResult FindFreePortCustomAction(Session session)
        {
            session.Log("Begin - FindFreePortCustomAction");

            int freePort = ParentControlCommon.Utils.GetFreeTcpPort();
            session["PORTNUMBER"] = Convert.ToString(freePort);

            session.Log("End - FindFreePortCustomAction");
            return ActionResult.Success;
        }

        /// <summary>
        /// Custom action for override the port and hostname in configuration file.
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        [CustomAction]
        public static ActionResult OverrideConfigCustomAction(Session session)
        {
            // Check uninstall flag.
            bool uninstallFlag = (session.CustomActionData.ContainsKey("UNINSTALLFLAG") && session.CustomActionData["UNINSTALLFLAG"] != null && session.CustomActionData["UNINSTALLFLAG"] == "1");
            if (uninstallFlag)
            {
                return ActionResult.Success;
            }

            session.Log("Begin - OverrideConfigCustomAction");

            String targetPath = session.CustomActionData["INSTALLFOLDER"];
            session.Log("OverrideConfigCustomAction - targetpath: " + targetPath);

            String hostname = session.CustomActionData["LISTENERHOST"];
            if (string.IsNullOrWhiteSpace(hostname))
            {
                hostname = "localhost";
            }
            session.Log("OverrideConfigCustomAction - hostname: " + hostname);
            String sPort = session.CustomActionData["PORTNUMBER"];
            if (string.IsNullOrWhiteSpace(sPort))
            {
                sPort = "5556";
            }
            int port = Convert.ToInt32(sPort);
            session.Log("OverrideConfigCustomAction - port: " + port);

            XmlDocument xmlDoc = new XmlDocument();
            string xmlDocPath = targetPath + "ParentControl.exe.config";
            session.Log("OverrideConfigCustomAction - config path: " + xmlDocPath);
            if (!File.Exists(xmlDocPath))
            {
                MessageBox.Show(session.CustomActionData["CouldNotWriteSettingsString"], session.CustomActionData["WarningString"], MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            xmlDoc.Load(xmlDocPath);
            session.Log("OverrideConfigCustomAction - config file loaded");

            XmlNode appSettingsNode = null;
            foreach (XmlNode xn in xmlDoc.ChildNodes)
            {
                if (xn.Name.Equals("configuration"))
                {
                    foreach (XmlNode xn2 in xn.ChildNodes)
                    {
                        if (xn2.Name.Equals("appSettings"))
                        {
                            appSettingsNode = xn2;
                            break;
                        }
                    }
                    break;                    
                }
            }
            
            session.Log("OverrideConfigCustomAction - configuration -> appSettings");
            foreach (XmlNode nodeAdd in appSettingsNode.ChildNodes)
            {
                if (nodeAdd.Name.Equals("add"))
                {
                    if (nodeAdd.Attributes["key"].Value.Equals("listenOn"))
                    {
                        nodeAdd.Attributes["value"].Value = hostname;
                    }
                    else if (nodeAdd.Attributes["key"].Value.Equals("port"))
                    {
                        nodeAdd.Attributes["value"].Value = port.ToString();
                    }
                }
            }
            xmlDoc.Save(xmlDocPath);

            session.Log("End - OverrideConfigCustomAction");
            return ActionResult.Success;
        }
    }
}
