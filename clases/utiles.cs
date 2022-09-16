using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Data;

namespace f2
{
    public class utiles
    {
        /// <summary>
        /// Devuelve un byte.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] getArrayByteFromStream(MemoryStream stream)
        {
            Stream fileStream = stream;
            Byte[] uploadBuffer = new byte[fileStream.Length];
            fileStream.Read(uploadBuffer, 0, (int)fileStream.Length);
            fileStream.Close();
            return uploadBuffer;
        }
        /// <summary>
        /// Devuelve un md5.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMD5(string str)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
        /// <summary>
        /// Convierte objectos a byte[].
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
        /// <summary>
        /// Convierte byte[] a objeto.
        /// </summary>
        /// <param name="arrBytes"></param>
        /// <returns></returns>
        private static Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);
            return obj;
        }
        /// <summary>
        /// Convierte byte[] a Base64.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string byteToBase64(byte[] data)
        {
            char[] base64data = new char[(int)(Math.Ceiling((double)data.Length / 3) * 4)];
            Convert.ToBase64CharArray(data, 0, data.Length, base64data, 0);
            return new String(base64data);
        }
        /// <summary>
        /// Convierte string a Base64.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static byte[] Base64ToByte(string data)
        {
            return Convert.FromBase64String(data);
        }
        public static int getCantidadEspacios(string cadenaAEvaluar)
        {
            int cantidadEspacios = 0;
            for (int i = 0; i < cadenaAEvaluar.Length; i++)
            {
                if (cadenaAEvaluar.Substring(i, 1).Equals(" "))
                {
                    cantidadEspacios++;
                }
            }
            return cantidadEspacios;
        }
    }
}
