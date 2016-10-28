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

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

#endregion Import namespace

namespace WorkData.Code.Helpers
{
    /// <summary>
    /// 图片帮助类
    /// </summary>
    /// <remarks>
    /// </remarks>
    public static class ImageHelper
    {
        /// <summary>
        /// 设置分辨率
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        /// <param name="xDpi"></param>
        /// <param name="yDpi"></param>
        /// <returns></returns>
        public static void SaveImage(Stream stream, string fileName, float xDpi, float yDpi)
        {
            using (var originImage = new Bitmap(stream))
            {
                var bmpNew = new Bitmap(originImage.Width, originImage.Height, PixelFormat.Format8bppIndexed);
                bmpNew.SetResolution(xDpi, yDpi);
                var g = Graphics.FromImage(originImage);

                var rec = new Rectangle();
                rec.Size = originImage.Size;

                g.DrawImage(bmpNew, new Point { X = 0, Y = 0 });

                bmpNew.Save(fileName, ImageFormat.Jpeg);
            }
        }
    }
}