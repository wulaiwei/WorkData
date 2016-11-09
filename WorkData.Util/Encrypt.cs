using System;
using System.Security.Cryptography;
using System.Text;

namespace WorkData.Util
{
    /// <summary>
    /// DES加密/解密类。
    /// </summary>
    public class Encrypt
    {
        #region 加密算法

        /// <summary>
        /// MD5码加密算法
        /// </summary>
        /// <param name="password"></param>
        /// <param name="md5Values"></param>
        /// <returns></returns>
        public static string Md5Hash(string password, string md5Values)
        {
            var md5 = MD5.Create();
            var sourceBytes = System.Text.Encoding.UTF8.GetBytes(password + md5Values);
            var resultBytes = md5.ComputeHash(sourceBytes);
            var buffer = new StringBuilder(resultBytes.Length);
            foreach (var b in resultBytes)
            {
                buffer.Append(b.ToString("x"));
            }
            return buffer.ToString();
        }

        #endregion 加密算法

        /// <summary>
        /// 生成随机字母字符串(数字字母混和)
        /// </summary>
        /// <param name="codeCount">待生成的位数</param>
        public static string GetCheckCode(int codeCount)
        {
            var str = string.Empty;
            var  rep = 0;
            var num2 = DateTime.Now.Ticks + rep;
            rep++;
            var random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> rep)));
            for (var i = 0; i < codeCount; i++)
            {
                char ch;
                var num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str = str + ch.ToString();
            }
            return str;
        }
    }
}