/* Created on 10.9.2016 */
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ParentControl.Utils;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ParentControl
{
    /// <summary>
    /// Listener class for client access.
    /// </summary>
    class ParentControlListener
    {
        /// <summary>
        /// Static instance of the listener.
        /// </summary>
        private static ParentControlListener instance;

        protected ILog logger = LogManager.GetLogger(typeof(ParentControlListener));

        /// <summary>
        /// HTTP listener for client access.
        /// </summary>
        private HttpListener httpListener;
        /// <summary>
        /// Listener port.
        /// </summary>
        private int? port = null;

        /// <summary>
        /// Default constructor.
        /// </summary>
        private ParentControlListener()
        {

            String listenOn = System.Configuration.ConfigurationSettings.AppSettings.Get("listenOn");
            listenOn = (listenOn != null ? listenOn : "localhost");

            String sPort = System.Configuration.ConfigurationSettings.AppSettings.Get("port");

            port = sPort != null ? Convert.ToInt32(sPort.Trim()) : ParentControlCommon.Utils.GetFreeTcpPort();

            using (FileStream fs = new FileStream(ParentControlCommon.Utils.ListenerPortCacheFile, FileMode.Create))
            {
                byte[] data = ASCIIEncoding.ASCII.GetBytes(Convert.ToString(port));
                fs.Write(data, 0, data.Length);
                fs.Flush();
                fs.Close();
            }

            httpListener = new HttpListener();
            httpListener.Prefixes.Add("http://" + listenOn + ":" + port + "/");
            foreach (string u in httpListener.Prefixes) {
                logger.Debug("HttpListener-prefix: " + u);
            }
        }

        /// <summary>
        /// Start method of the listener.
        /// </summary>
        public void Startup()
        {
            logger.Info("Listener starting up ...");
            httpListener.Start();
            new Thread(ThreadRun).Start();
        }

        /// <summary>
        /// Run method of the listener.
        /// </summary>
        private void ThreadRun()
        {
            HttpListenerContext context;
            HttpListenerRequest request;
            HttpListenerResponse response;
            while (httpListener != null && httpListener.IsListening)
            {
                context = httpListener.GetContext();
                request = context.Request;
                response = context.Response;
                DoRequest(request, response);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        private void DoRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            logger.Debug("REQUEST_URL: " + request.Url.ToString());

            byte[] data = new byte[] { };
            try
            {
                RequestParams reqParams = new RequestParams();
                reqParams.ParseTo(request);

                Assembly asm = Assembly.GetEntryAssembly();
                int idx = reqParams.MethodName.LastIndexOf('.');
                if (idx >= 0)
                {
                    string classForMethod = asm.GetName().Name + ".Business." + reqParams.MethodName.Substring(0, idx).Trim();
                    object classInstance = asm.CreateInstance(classForMethod);

                    MethodInfo method = classInstance.GetType().GetMethod(reqParams.MethodName.Substring(idx + 1).Trim());
                    if (method == null) {
                        throw new InvalidOperationException(
                            "Could not found method '" + classForMethod + "." 
                            + reqParams.MethodName.Substring(idx + 1).Trim() + "'.");
                    }

                    if (method.GetParameters().Length > 1) {
                        throw new InvalidOperationException("Business method '" + classForMethod + "."
                            + reqParams.MethodName.Substring(idx + 1).Trim() 
                            + "' could not contain more than 1 parameter");
                    }

                    Object param = null;
                    if (reqParams.Content != null && reqParams.Content.Trim().Length > 0 && method.GetParameters().Length == 1)
                    {
                        param = JsonConvert.DeserializeObject(reqParams.Content, method.GetParameters()[0].ParameterType);
                    }
                    else if (reqParams.PathParams != null && reqParams.PathParams.Count > 0 && method.GetParameters().Length == 1)
                    {
                        param = Convert.ChangeType(reqParams.PathParams[0], method.GetParameters()[0].ParameterType);
                    }

                    try {
                        Object result = method.Invoke(classInstance, (param != null ? new object[] { param } : null));

                        if (method.ReturnType != typeof(void))
                        {
                            String jsonResult = JsonConvert.SerializeObject(result);
                            // FIXME Zakriptovanie odosielanej spravy ...
                            // ENC -> jsonResult = Convert.ToBase64String(ParentControlCommon.Utils.EncryptAES(UTF8Encoding.UTF8.GetBytes(jsonResult)));
                            // DEC -> jsonResult = UTF8Encoding.UTF8.GetString(ParentControlCommon.Utils.DecryptAES(Convert.FromBase64String(jsonResult)));
                            //jsonResult = Convert.ToBase64String(ParentControlCommon.Utils.EncryptAES(UTF8Encoding.UTF8.GetBytes(jsonResult)));
                            //data = Encoding.ASCII.GetBytes(jsonResult);
                            data = Encoding.UTF8.GetBytes(jsonResult);
                        }

                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.StatusDescription = "OK";
                    } catch (TargetInvocationException ex) {
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);

                ObjectModel.Exception excp = new ObjectModel.Exception();
                excp.TypeException = ObjectModel.Exception.ERROR;
                if (typeof(WarningException).IsInstanceOfType(ex)) {
                    excp.TypeException = ObjectModel.Exception.WARNING;
                }
                excp.Message = ex.Message;

                JObject jobj = JObject.FromObject(excp);
                String jsonResult = jobj.ToString();
                // FIXME Doriesit kriptovanie ...
                jsonResult = Convert.ToBase64String(ParentControlCommon.Utils.EncryptAES(UTF8Encoding.UTF8.GetBytes(jsonResult)));
                data = Encoding.ASCII.GetBytes(jsonResult);
                response.StatusCode = (int)HttpStatusCode.Forbidden;
                response.StatusDescription = "Request failure.";
            }

            response.ContentType = "application/json";
            response.OutputStream.Write(data, 0, data.Length);
            response.OutputStream.Close();
        }

        /// <summary>
        /// Shutdown method of the listener.
        /// </summary>
        public void Shutdown()
        {
            try {
                logger.Info("Listener shutting down ...");
                httpListener.Close();
            } finally {
                if (File.Exists(ParentControlCommon.Utils.ListenerPortCacheFile))
                {
                    File.Delete(ParentControlCommon.Utils.ListenerPortCacheFile);
                }
            }

        }

        /// <summary>
        /// Getter for static instance.
        /// </summary>
        public static ParentControlListener Instance {
            get {
                if (instance == null) {
                    instance = new ParentControlListener();
                }
                return instance;
            }
        }

    }

}
