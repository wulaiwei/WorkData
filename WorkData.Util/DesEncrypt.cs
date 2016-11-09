#region 版权信息

// ------------------------------------------------------------------------------
//  Copyright (C) 成都联宇创新科技有限公司 版权所有。
//
//  文件名：Des.cs
//  文件功能描述：
//  创建标识：吴来伟
//
//  修改标识：
//  修改描述：
// ------------------------------------------------------------------------------

#endregion 版权信息

#region 名称空间导入

using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

#endregion 名称空间导入

namespace WorkData.Util
{
    /// <summary>
    ///     DES加密/解密类。
    /// </summary>
    public class DesEncrypt
    {
        #region ========加密========

        /// <summary>
        ///     加密
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Encrypt(string text)
        {
            return Encrypt(text, "Mr.Wu");
        }

        /// <summary>
        ///     加密数据
        /// </summary>
        /// <param name="text"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string Encrypt(string text, string sKey)
        {
            using (var des = new DESCryptoServiceProvider())
            {
                var inputByteArray = Encoding.Default.GetBytes(text);
                des.Key = Encoding.Default.GetBytes(HashPassword(sKey, HashFromat.Md5)
                                                        .Substring(0, 8));
                des.IV = Encoding.Default.GetBytes(HashPassword(sKey, HashFromat.Md5)
                                                       .Substring(0, 8));
                var memoryStream = new MemoryStream();
                var stream = new CryptoStream(memoryStream, des.CreateEncryptor(), CryptoStreamMode.Write);
                stream.Write(inputByteArray, 0, inputByteArray.Length);
                stream.FlushFinalBlock();
                var builder = new StringBuilder();
                foreach (var b in memoryStream.ToArray())
                {
                    builder.AppendFormat("{0:X2}", b);
                }
                return builder.ToString();
            }
        }

        #endregion ========加密========

        #region ========解密========

        /// <summary>
        ///     解密
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Decrypt(string text)
        {
            return Decrypt(text, "Mr.Luo");
        }

        /// <summary>
        ///     解密数据
        /// </summary>
        /// <param name="text"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string Decrypt(string text, string sKey)
        {
            var len = text.Length / 2;
            var inputByteArray = new byte[len];
            for (var x = 0; x < len; x++)
            {
                var i = Convert.ToInt32(text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            using (var des = new DESCryptoServiceProvider())
            {
                des.Key = Encoding.ASCII.GetBytes(HashPassword(sKey, HashFromat.Md5)
                                                      .Substring(0, 8));
                des.IV = Encoding.ASCII.GetBytes(HashPassword(sKey, HashFromat.Md5)
                                                     .Substring(0, 8));
                var memoryStream = new MemoryStream();
                var cs = new CryptoStream(memoryStream, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Encoding.Default.GetString(memoryStream.ToArray());
            }
        }

        #endregion ========解密========

        #region 编码转换

        /// <summary>
        ///     HEX 转换为 INT
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        private static int HexToInt(char h)
        {
            return (h >= '0' && h <= '9')
                ? h - '0'
                : (h >= 'a' && h <= 'f')
                    ? h - 'a' + 10
                    : (h >= 'A' && h <= 'F')
                        ? h - 'A' + 10
                        : -1;
        }

        /// <summary>
        ///     Int 转换为 Hex
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private static char IntToHex(int n)
        {
            Debug.Assert(n < 0x10);

            if (n <= 9)
            {
                return (char)(n + '0');
            }
            return (char)(n - 10 + 'a');
        }

        // 将半个字节(4 bits)转换为对应的大写的十六进制字符表示 [0-9, A-F]
        private static char NibbleToHex(byte nibble)
        {
            return (char)((nibble < 10) ? (nibble + '0') : (nibble - 10 + 'A'));
        }

        /// <summary>
        ///     一个字节数组转换为十六进制表示形式转换
        /// </summary>
        /// <param name="data">The binary byte array.</param>
        /// <returns>The hexadecimal (uppercase) equivalent of the byte array.</returns>
        private static string BinaryToHex(byte[] data)
        {
            if (data == null)
            {
                return null;
            }

            var hex = new char[checked(data.Length * 2)];

            for (var i = 0; i < data.Length; i++)
            {
                var thisByte = data[i];
                hex[2 * i] = NibbleToHex((byte)(thisByte >> 4)); // high nibble
                hex[2 * i + 1] = NibbleToHex((byte)(thisByte & 0xf)); // low nibble
            }

            return new string(hex);
        }

        /// <summary>
        ///     一个十六进制的字符串转换成它的二进制表示。
        /// </summary>
        /// <param name="data">十六进制的字符串</param>
        /// <returns>对应的十六进制字符串，字符串的内容的字节数组或者如果输入的字符串不是有效的十六进制字符串，则为 null。</returns>
        private static byte[] HexToBinary(string data)
        {
            if (data == null
                || data.Length % 2 != 0)
            {
                // input string length is not evenly divisible by 2
                return null;
            }

            var binary = new byte[data.Length / 2];

            for (var i = 0; i < binary.Length; i++)
            {
                var highNibble = HexToInt(data[2 * i]);
                var lowNibble = HexToInt(data[2 * i + 1]);

                if (highNibble == -1
                    || lowNibble == -1)
                {
                    return null; // bad hex data
                }
                binary[i] = (byte)((highNibble << 4) | lowNibble);
            }

            return binary;
        }

        #endregion 编码转换

        #region Hash运算

        /// <summary>
        ///     对密码做 Hash运算
        /// </summary>
        /// <param name="password">密码</param>
        /// <param name="hashFromat">哈希算法</param>
        /// <returns></returns>
        private static string HashPassword(string password, HashFromat hashFromat)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (password == null)
            {
                throw new ArgumentNullException("密码不能为空");
            }

            HashAlgorithm hashAlgorithm = new MD5Cng();
            switch (hashFromat)
            {
                case HashFromat.Md5:
                    hashAlgorithm = new MD5Cng();
                    break;

                case HashFromat.Sha1:
                    hashAlgorithm = new SHA1Cng();
                    break;
            }

            using (hashAlgorithm)
            {
                return BinaryToHex(hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }

        /// <summary>
        ///     Hash 算法
        /// </summary>
        private enum HashFromat
        {
            /// <summary>
            ///     MD5
            /// </summary>
            Md5 = 0,

            /// <summary>
            ///     SHA1
            /// </summary>
            Sha1
        }

        #endregion Hash运算
    }
}