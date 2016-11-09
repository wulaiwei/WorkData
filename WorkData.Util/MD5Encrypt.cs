using System.Security.Cryptography;
using System.Text;

namespace WorkData.Util
{
    public class Md5Encrypt
    {
        #region 加密算法

        /// <summary>
        /// MD5码加密算法
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string Md5Hash(string password)
        {
            var md5 = MD5.Create();
            var sourceBytes = Encoding.UTF8.GetBytes(password);
            var resultBytes = md5.ComputeHash(sourceBytes);
            var buffer = new StringBuilder(resultBytes.Length);
            foreach (var b in resultBytes)
            {
                buffer.Append(b.ToString("x"));
            }
            return buffer.ToString();
        }

        #endregion 加密算法
    }
}