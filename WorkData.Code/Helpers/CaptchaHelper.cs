#region Apache License Version 2.0

// ---------------------------------------------------------------------------
//  Copyright 2016  The LightWork Project
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
// ---------------------------------------------------------------------------

#endregion Apache License Version 2.0

#region Import namespace

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

#endregion Import namespace

namespace WorkData.Code.Helpers
{
    /// <summary>
    /// 图片验证码
    /// </summary>
    /// <remarks>
    /// CompletelyAutomatedPublicTuringtesttotellComputersandHumansApart
    /// 全自动区分计算机和人类的图灵测试
    /// </remarks>
    public static class CaptchaHelper
    {
        /// <summary>
        /// 验证码SessionKey
        /// </summary>
        private const string SessionKey = "ValidateCode";

        /// <summary>
        /// 验证码图片内容类型
        /// </summary>
        public const string ContentType = "image/gif";

        private static readonly char[] CharArray =
        {
            '2', '3', '4', '5', '6', '8', '9',
            'a', 'b', 'c', 'd', 'e', 'f', 'g','h', 'j', 'k', 'm', 'n', 'p', 'r', 's', 'u', 'w', 'x', 'y',
            'A', 'B', 'C', 'D', 'E', 'F', 'G','H', 'J', 'K', 'M', 'N', 'P', 'R', 'S', 'U', 'W', 'X', 'Y',
        };

        private static readonly Color[] ColorList =
        {
            Color.Red,
            Color.Orange,
            Color.Brown,
            Color.Green,
            Color.DarkCyan,
            Color.Blue,
            Color.Purple
        };

        /// <summary>
        /// 创建验证码（image/gif）
        /// </summary>
        /// <param name="charCount">验证码字符数</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns></returns>
        public static byte[] CreateCaptcha(int charCount, int width, int height)
        {
            var validateCode = CreateRandomCode(charCount);
            HttpContext.Current.Session[SessionKey] = validateCode;
            var imgBuffer = CreateImage(validateCode, width, height);

            return imgBuffer;
        }

        /// <summary>
        /// 验证验证码
        /// </summary>
        /// <param name="validateCode"></param>
        /// <param name="ignoreCase">忽略大小写</param>
        /// <returns></returns>
        public static VerifyResult VerifyCaptcha(string validateCode, bool ignoreCase = true)
        {
            var session = HttpContext.Current.Session;
            if (session?[SessionKey] == null || validateCode == null)
            {
                return CreateResult(false);
            }
            var holdCode = Convert.ToString(session[SessionKey]);
            var result = string.Equals(holdCode, validateCode, ignoreCase
                ? StringComparison.CurrentCultureIgnoreCase
                : StringComparison.CurrentCulture);

            return CreateResult(result);
        }

        private static VerifyResult CreateResult(bool value)
        {
            var result = new VerifyResult { State = value };

            return result;
        }

        /// <summary>
        /// 创建验证码图片
        /// </summary>
        /// <param name="checkCode"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private static byte[] CreateImage(string checkCode, int width, int height)
        {
            using (var image = new Bitmap(width, height))
            {
                using (var graph = Graphics.FromImage(image))
                {
                    graph.Clear(Color.White); // 填充文字

                    var font = new Font("Arial", 32, (FontStyle.Italic), GraphicsUnit.Pixel); // 字体
                    for (var i = 0; i < checkCode.Length; i++)
                    {
                        var str = Convert.ToString(checkCode[i]);
                        var colorIndex = RandomHelper.Next(0, 7);
                        Brush brush = new SolidBrush(ColorList[colorIndex]); // 字体颜色
                        graph.DrawString(str, font, brush, 5 + i * 24, 1);
                    }

                    #region 随机正弦/余弦 波形滤镜

                    var distortion = RandomHelper.Next(0, 1) == 0;
                    var waveFrom = RandomHelper.Next(1, 2);
                    var extenValue = RandomHelper.Next(3, 5);
                    var beginPhase = RandomHelper.Next(3, 6);

                    var destImage = WaveFilter(image, distortion, waveFrom, extenValue, beginPhase);

                    #endregion 随机正弦/余弦 波形滤镜

                    using (var memoryStream = new MemoryStream())
                    {
                        destImage.Save(memoryStream, ImageFormat.Gif);
                        var buffer = memoryStream.ToArray();

                        return buffer;
                    }
                }
            }
        }

        /// <summary>
        /// 生成验证码字符串
        /// </summary>
        /// <param name="length">位数</param>
        /// <returns>验证码字符串</returns>
        private static string CreateRandomCode(int length)
        {
            var randomCode = new List<char>(length);
            do
            {
                var pos = RandomHelper.Next(CharArray.Length);
                var code = CharArray[pos];
                if (randomCode.Contains(code))
                {
                    continue;
                }
                randomCode.Add(code);
            } while (randomCode.Count < length);

            return string.Join("", randomCode);
        }

        #region 产生波形滤镜效果

        /// <summary>
        /// 波形滤镜
        /// </summary>
        /// <param name="originBmp">原图片</param>
        /// <param name="distortion">如果扭曲则选择为True</param>
        /// <param name="waveform">波形类别 1=正弦波 2=余弦波</param>
        /// <param name="extenValue">波形的幅度倍数，越大扭曲的程度越高，一般为3</param>
        /// <param name="beginPhase">波形的起始相位，取值区间[0-2PI)</param>
        /// <returns></returns>
        private static Bitmap WaveFilter(Bitmap originBmp, bool distortion, int waveform, double extenValue, double beginPhase)
        {
            //相位限制
            const double phaseLimit = Math.PI * 2;
            var destBmp = new Bitmap(originBmp.Width, originBmp.Height);
            // 将位图背景填充为白色
            using (var graph = Graphics.FromImage(destBmp))
            {
                graph.Clear(Color.White);
                var dBaseAxisLen = distortion ? destBmp.Height : (double)destBmp.Width;
                for (var i = 0; i < destBmp.Width; i++)
                {
                    for (var j = 0; j < destBmp.Height; j++)
                    {
                        var destX = (distortion ? (phaseLimit * j) / dBaseAxisLen : (phaseLimit * i) / dBaseAxisLen) +
                                    beginPhase;
                        var destY = waveform == 1 ? Math.Sin(destX) : Math.Cos(destX);

                        // 取得当前点的颜色
                        var oldX = distortion ? i + (int)(destY * extenValue) : i;
                        var oldY = distortion ? j : j + (int)(destY * extenValue);

                        var color = originBmp.GetPixel(i, j);
                        if (oldX >= 0 && oldX < destBmp.Width && oldY >= 0 && oldY < destBmp.Height)
                        {
                            destBmp.SetPixel(oldX, oldY, color);
                        }
                    }
                }

                graph.DrawRectangle(new Pen(Color.LightGray), 0, 0, destBmp.Width - 1, destBmp.Height - 1);

                return destBmp;
            }
        }

        #endregion 产生波形滤镜效果

        public struct VerifyResult
        {
            public bool State { get; set; }
        }
    }
}