/* Created on 21.9.2016 */
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace ParentControlCommon
{
    /// <summary>
    /// Global utility class.
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Constrant size for decrypting.
        /// </summary>
        public const int DECRYPT_BUFFER = 3072;

        /// <summary>
        /// Keys for enc/dec.
        /// </summary>
        private static byte[] key = ASCIIEncoding.ASCII.GetBytes("m3iPaQ6t4H63ofSS");
        private static byte[] iv = ASCIIEncoding.ASCII.GetBytes("QM5D2af4Fqo811mp");

        /// <summary>
        /// Get next free TCP port.
        /// </summary>
        /// <returns></returns>
        public static int GetFreeTcpPort()
        {
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            return port;
        }

        /// <summary>
        /// Encrypting ...
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] EncryptAES(byte[] data)
        {
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, Aes.Create().CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    csEncrypt.Write(data, 0, data.Length);
                    csEncrypt.FlushFinalBlock();
                    return msEncrypt.ToArray();
                }
            }
        }

        /// <summary>
        /// Decrypting ...
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] DecryptAES(byte[] data)
        {
            Aes aes = Aes.Create();
            aes.Padding = PaddingMode.None;

            using (MemoryStream msDecrypt = new MemoryStream(data))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        csDecrypt.CopyTo(ms);
                        ms.Position = 0;
                        StreamReader r = new StreamReader(ms);
                        string s = r.ReadToEnd();
                        return Encoding.UTF8.GetBytes(s);
                    }
                }
            }
        }

        /// <summary>
        /// Getting absolute path of the database folder.
        /// </summary>
        public static String DatabaseFolder {
            get {
                return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\ParentControl";
            }
        }

        /// <summary>
        /// Getter for 'ListenerPortCache' file name.
        /// </summary>
        public static String ListenerPortCacheFile {
            get {
                return DatabaseFolder + @"\Listener.Port.cache";
            }
        }

    }

}
