/* Created on 11.9.2016 */
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;

namespace ParentControl.Utils
{
    /// <summary>
    /// Structure for request parameters.
    /// </summary>
    public class RequestParams
    {
        public const int BUFFER_SIZE = (256 * 1024);

        private String methodName;
        private IList<String> pathParams = new List<String>();
        private IDictionary<String, String> queryParams = new Dictionary<String, String>();
        private String content;

        /// <summary>
        /// Getter for method name.
        /// </summary>
        public String MethodName {
            get {
                return methodName;
            }
        }

        /// <summary>
        /// Getter of the path parameters.
        /// </summary>
        public IList<String> PathParams {
            get {
                return pathParams;
            }
        }

        /// <summary>
        /// Getter for query's parameters.
        /// </summary>
        public IDictionary<String, String> QueryParams {
            get {
                return queryParams;
            }
        }

        /// <summary>
        /// Getter for content.
        /// </summary>
        public String Content {
            get {
                return content;
            }
        }

        /// <summary>
        /// This method parses request into needed fields.
        /// </summary>
        /// <param name="request"></param>
        public void ParseTo(HttpListenerRequest request)
        {
            // Reading of the content.
            content = null;
            if (request.ContentLength64 > 0L)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] buffer = new byte[BUFFER_SIZE];
                    int readed;
                    while ((readed = request.InputStream.Read(buffer, 0, BUFFER_SIZE)) > 0)
                    {
                        ms.Write(buffer, 0, readed);
                    }
                    ms.Flush();
                    content = request.ContentEncoding.GetString(ms.ToArray());
                    ms.Close();
                }
            }

            // Reading of the method name and path parameters.
            methodName = null;
            pathParams.Clear();
            String s2;
            foreach (String s in request.Url.Segments)
            {
                if (s == "/") continue;

                s2 = (s.EndsWith("/") ? s.Substring(0, s.Length - 1) : s);

                if (methodName == null)
                {
                    methodName = s2;
                }
                else
                {
                    pathParams.Add(s2);
                }
            }

            // Query parameters.
            queryParams.Clear();
            foreach (String key in request.QueryString)
            {
                queryParams.Add(key, request.QueryString[key]);
            }
        }

    }

}
