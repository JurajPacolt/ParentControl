/* Created on 18.9.2016 */
using log4net;
using Newtonsoft.Json;
using ParentControlCommon;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParentControlClient.Remote.Base
{
    /// <summary>
    /// Remote communication factory.
    /// </summary>
    public class RemoteCommFactory
    {
        protected ILog logger = LogManager.GetLogger(typeof(RemoteCommFactory));

        /// <summary>
        /// Will be only static instance.
        /// </summary>
        private static RemoteCommFactory instance;

        private HttpClient httpClient;
        private String baseUrl;
        private int port;

        private readonly object syncLock = new object();

        /// <summary>
        /// Default constructor.
        /// </summary>
        private RemoteCommFactory() {
            Renew();
        }

        /// <summary>
        /// Renew HTTP client.
        /// </summary>
        private void Renew()
        {
            if (httpClient != null)
            {
                httpClient.Dispose();
                httpClient = null;
            }

            if (!File.Exists(ParentControlCommon.Utils.ListenerPortCacheFile))
            {
                throw new InvalidOperationException("Cache file with server port is not exists.");
            }

            // Read server listener port. It's dynamic.
            using (FileStream fs = new FileStream(ParentControlCommon.Utils.ListenerPortCacheFile, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    port = Convert.ToInt32(sr.ReadLine().Trim());
                }
            }

            if (!CheckListening())
            {
                throw new InvalidOperationException("Nothing listen on port " + port + ". Is possible that server is not running.");
            }

            httpClient = new HttpClient();
            baseUrl = "http://localhost:" + port;
            httpClient.BaseAddress = new Uri(baseUrl);
        }

        /// <summary>
        /// Check if on port is listening.
        /// </summary>
        /// <returns></returns>
        private Boolean CheckListening()
        {
            try {
                using (TcpClient tc = new TcpClient())
                {
                    tc.Connect("localhost", port);
                    bool stat = tc.Connected;
                    if (stat)
                    {
                        return true;
                    }
                }
            } catch (Exception) {
            }
            return false;
        }

        /// <summary>
        /// Remote calling.
        /// </summary>
        /// <param name="methodName">Method name on server side.</param>
        /// <param name="returnType">Return type.</param>
        /// <param name="requestParams">Request object.</param>
        /// <param name="pathParams">Optional path parameters.</param>
        /// <returns></returns>
        public Object Call(String methodName, Type returnType, Object requestParams, params object[] pathParams)
        {
            lock (syncLock)
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    using (HttpContent cont = (
                        requestParams != null
                        ? new StringContent(JsonConvert.SerializeObject(requestParams), Encoding.UTF8, "application/json")
                        : null))
                    {
                        StringBuilder sb = new StringBuilder(baseUrl + "/" + methodName);
                        foreach (object pathParam in pathParams)
                        {
                            if (pathParam == null)
                            {
                                throw new InvalidOperationException("Parameter in path could not be NULL.");
                            }
                            sb.Append("/").Append(pathParam.ToString());
                        }
                        using (HttpResponseMessage resp = (
                            requestParams != null
                            ? httpClient.PostAsync(sb.ToString(), cont).Result
                            : httpClient.GetAsync(sb.ToString()).Result))
                        {
                            String json = resp.Content.ReadAsStringAsync().Result;
                            // FIXME Odkriptovanie doslej spravy ...
                            //String json = UTF8Encoding.UTF8.GetString(Utils.DecryptAES(Convert.FromBase64String(response)));
                            if (resp.StatusCode == HttpStatusCode.OK)
                            {
                                return JsonConvert.DeserializeObject(json, returnType);
                            }
                            else
                            {
                                ParentControl.ObjectModel.Exception exception =
                                    (ParentControl.ObjectModel.Exception)JsonConvert.DeserializeObject(
                                        json, typeof(ParentControl.ObjectModel.Exception));
                                switch (exception.TypeException)
                                {
                                    case ParentControl.ObjectModel.Exception.WARNING:
                                        logger.Warn(exception.Message);
                                        throw new WarningException(exception.Message);
                                    case ParentControl.ObjectModel.Exception.ERROR:
                                        logger.Error(exception.Message);
                                        throw new InvalidOperationException(exception.Message);
                                    default:
                                        logger.Error(exception.Message);
                                        throw new InvalidOperationException(exception.Message);
                                }
                            }
                        }
                    }
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// Getter for static instance of the factory.
        /// </summary>
        public static RemoteCommFactory Instance {
            get {
                if (instance == null) {
                    instance = new RemoteCommFactory();
                }
                return instance;
            }
        }
    }

}
