using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using WorkData.Web.Areas.Admin.Models;
using WorkData.Util;
using static System.String;

namespace WorkData.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 文件操作
    /// </summary>
    public class FileController : Controller
    {
        private readonly SiteConfig _siteConfig;

        public FileController()
        {
            _siteConfig = new SiteConfig();
        }

        /// <summary>
        /// 工厂
        /// </summary>
        /// <returns></returns>
        public string ProcessRequest()
        {
            var request = System.Web.HttpContext.Current.Request;
            var action = request.QueryString["Action"];
            var msg = "";
            switch (action)
            {
                case "UpLoadFile": //普通上传
                    msg = UpLoadFile(request);
                    break;
                default: //编辑器文件
                    msg = EditorFile(request);
                    break;
            }
            return msg;
        }

        #region 编辑器上传
        private string EditorFile(HttpRequest request)
        {
            var iswater = false || request.QueryString["IsWater"] == "1"; //默认不打水印
            /*图片属性（高和宽）*/
            var imgWidth = string.IsNullOrWhiteSpace(request.QueryString["ImgWidth"]) ? 0 :
                Convert.ToInt32(request.QueryString["ImgWidth"]);
            var imgHeight = string.IsNullOrWhiteSpace(request.QueryString["ImgHeight"]) ? 0 :
                Convert.ToInt32(request.QueryString["ImgHeight"]);
            var imgFile = request.Files["imgFile"];
            string msg;
            if (imgFile == null)
            {
                msg = "{\"status\": 0, \"msg\": \"请选择要上传文件！\"}";
                return msg;
            }

            var remsg = FileSaveAs(imgFile, false, false);
            var jd = JsonConvert.DeserializeObject<Dictionary<string, string>>(remsg);
            var status = jd["status"];
            var uploadMsg = jd["msg"];
            if (status == "0")
            {
                msg = "{\"status\": 0, \"msg\": \""+uploadMsg+"\"}";
                return msg;
            }
            /*
             文件路径处理：
             * 如果图片的压缩图片地址不为空的话，则获取压缩地址
             * 否则就获取图片的原地址
             */
            var filePath = jd["path"];
            var hash = new Hashtable
            {
                ["error"] = 0,
                ["url"] = filePath
            };
            return JsonConvert.SerializeObject(hash);
        }

        #endregion

        #region 普通上传
        /// <summary>
        ///  普通上传
        /// </summary>
        /// <param name="request"></param>
        private string UpLoadFile(HttpRequest request)
        {
            var delfile = request.QueryString["DelFilePath"];
            var upfile = request.Files["Filedata"];
            /*图片属性（高和宽）*/
            var imgWidth = IsNullOrWhiteSpace(request.QueryString["ImgWidth"]) ? 0
                : Convert.ToInt32(request.QueryString["ImgWidth"]);
            var imgHeight = IsNullOrWhiteSpace(request.QueryString["ImgHeight"]) ? 0
                : Convert.ToInt32(request.QueryString["ImgHeight"]);
            var iswater = false; //默认不打水印
            var isthumbnail = false; //默认不生成缩略图

            if (request.QueryString["IsWater"] == "1")
                iswater = true;
            if (request.QueryString["IsThumbnail"] == "1")
                isthumbnail = true;

            string msg;
            if (upfile == null)
            {
                msg = "{\"status\": 0, \"msg\": \"请选择要上传文件！\"}";
                return msg;
            }

            /*如果有规定裁剪大小的话，则使用FileSaveAs方法进行处理*/
            if (imgWidth > 0 && imgHeight > 0)
            {
                msg = FileSaveAs(upfile, isthumbnail, iswater, imgWidth, imgHeight, false);
            }
            else
            {
                msg = FileSaveAs(upfile, isthumbnail, iswater);
            }

            //删除已存在的旧文件，旧文件不为空且应是上传文件，防止跨目录删除
            if (!IsNullOrEmpty(delfile) && delfile.IndexOf("../", StringComparison.Ordinal) == -1
                && delfile.ToLower().StartsWith(_siteConfig.Webpath + _siteConfig.Filepath.ToLower()))
            {
                Utils.DeleteUpFile(delfile);
            }
            return msg;
        } 
        #endregion


        /// <summary>
        /// 文件上传方法
        /// </summary>
        /// <param name="postedFile">文件流</param>
        /// <param name="isThumbnail">是否生成缩略图</param>
        /// <param name="isWater">是否打水印</param>
        /// <returns>上传后文件信息</returns>
        private string FileSaveAs(HttpPostedFile postedFile, bool isThumbnail, bool isWater)
        {
            try
            {
                var fileExt = Utils.GetFileExt(postedFile.FileName); //文件扩展名，不含“.”
                var fileSize = postedFile.ContentLength; //获得文件大小，以字节为单位
                var fileName = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf(@"\", StringComparison.Ordinal) + 1); //取得原文件名
                var newFileName = Utils.GetRamCode() + "." + fileExt; //随机生成新的文件名
                var newThumbnailFileName = "thumb_" + newFileName; //随机生成缩略图文件名
                var upLoadPath = GetUpLoadPath(); //上传目录相对路径
                var fullUpLoadPath = Utils.GetMapPath(upLoadPath); //上传目录的物理路径
                var newFilePath = upLoadPath + newFileName; //上传后的路径
                var newThumbnailPath = upLoadPath + newThumbnailFileName; //上传后的缩略图路径

                //检查文件扩展名是否合法
                if (!CheckFileExt(fileExt))
                {
                    return "{\"status\": 0, \"msg\": \"不允许上传" + fileExt + "类型的文件！\"}";
                }
                //检查文件大小是否合法
                if (!CheckFileSize(fileExt, fileSize))
                {
                    return "{\"status\": 0, \"msg\": \"文件超过限制的大小啦！\"}";
                }
                //检查上传的物理路径是否存在，不存在则创建
                if (!Directory.Exists(fullUpLoadPath))
                {
                    Directory.CreateDirectory(fullUpLoadPath);
                }

                //保存文件
                postedFile.SaveAs(fullUpLoadPath + newFileName);
                //如果是图片，检查图片是否超出最大尺寸，是则裁剪
                if (IsImage(fileExt) && (_siteConfig.Imgmaxheight > 0 || _siteConfig.Imgmaxwidth > 0))
                {
                    Thumbnail.MakeThumbnailImage(fullUpLoadPath + newFileName, fullUpLoadPath + newFileName,
                        _siteConfig.Imgmaxwidth, _siteConfig.Imgmaxheight);
                }
                //如果是图片，检查是否需要生成缩略图，是则生成
                if (IsImage(fileExt) && isThumbnail && _siteConfig.Thumbnailwidth > 0 && _siteConfig.Thumbnailheight > 0)
                {
                    Thumbnail.MakeThumbnailImage(fullUpLoadPath + newFileName, fullUpLoadPath + newThumbnailFileName,
                        _siteConfig.Thumbnailwidth, _siteConfig.Thumbnailheight, "Cut");
                }
                //如果是图片，检查是否需要打水印
                if (IsWaterMark(fileExt) && isWater)
                {
                    switch (_siteConfig.Watermarktype)
                    {
                        case 1:
                            WaterMark.AddImageSignText(newFilePath, newFilePath,
                                _siteConfig.Watermarktext, _siteConfig.Watermarkposition,
                                _siteConfig.Watermarkimgquality, _siteConfig.Watermarkfont, _siteConfig.Watermarkfontsize);
                            break;

                        case 2:
                            WaterMark.AddImageSignPic(newFilePath, newFilePath,
                                _siteConfig.Watermarkpic, _siteConfig.Watermarkposition,
                                _siteConfig.Watermarkimgquality, _siteConfig.Watermarktransparency);
                            break;
                    }
                }
                //处理完毕，返回JOSN格式的文件信息
                return "{\"status\": 1, \"msg\": \"上传文件成功！\", \"name\": \""
                    + fileName + "\", \"path\": \"" + newFilePath + "\", \"thumb\": \""
                    + newThumbnailPath + "\", \"size\": " + fileSize + ", \"ext\": \"" + fileExt + "\"}";
            }
            catch
            {
                return "{\"status\": 0, \"msg\": \"上传过程中发生意外错误！\"}";
            }
        }

        /// <summary>
        /// 文件上传方法-重载
        /// 创建：MJZ
        /// 时间：2014年7月30日 19:16:39
        /// </summary>
        /// <param name="postedFile">文件流</param>
        /// <param name="isThumbnail">是否生成缩略图</param>
        /// <param name="isWater">是否打水印</param>
        /// <param name="imgWidth">图片压缩宽度</param>
        /// <param name="imgHeight">图片压缩高度</param>
        /// <param name="isSaveFullPath">是否保存全路径</param>
        /// <returns>上传后文件信息</returns>

        public string FileSaveAs(HttpPostedFile postedFile, bool isThumbnail, bool isWater, int imgWidth, int imgHeight, bool isSaveFullPath)
        {
            try
            {
                var fileExt = Utils.GetFileExt(postedFile.FileName); //文件扩展名，不含“.”
                var fileSize = postedFile.ContentLength; //获得文件大小，以字节为单位
                var fileName = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf(@"\", StringComparison.Ordinal) + 1); //取得原文件名
                var newFileName = Utils.GetRamCode() + "." + fileExt; //随机生成新的文件名
                var newThumbnailFileName = "thumb_" + newFileName; //随机生成缩略图文件名
                var upLoadPath = GetUpLoadPath(); //上传目录相对路径
                var fullUpLoadPath = Utils.GetMapPath(upLoadPath); //上传目录的物理路径
                var newFilePath = upLoadPath + newFileName; //上传后的路径
                var newThumbnailPath = upLoadPath + newThumbnailFileName; //上传后的缩略图路径
                /*
                 *不需要检查文件大小和类型，因为控件已经检查过了。
                 *检查上传的物理路径是否存在，不存在则创建
                 */
                if (!Directory.Exists(fullUpLoadPath))
                {
                    Directory.CreateDirectory(fullUpLoadPath);
                }
                //保存文件
                postedFile.SaveAs(fullUpLoadPath + newFileName);
                //如果是图片，检查是否需要生成缩略图，是则生成
                if (IsImage(fileExt) && isThumbnail && imgWidth > 0 && imgHeight > 0)
                {
                    Thumbnail.MakeThumbnailImage(fullUpLoadPath + newFileName, fullUpLoadPath + newThumbnailFileName,
                        imgWidth, imgHeight, "W");
                }
                //如果是图片，检查是否需要打水印
                if (IsWaterMark(fileExt) && isWater)
                {
                    switch (_siteConfig.Watermarktype)
                    {
                        case 1:
                            WaterMark.AddImageSignText(newFilePath, newFilePath,
                                _siteConfig.Watermarktext, _siteConfig.Watermarkposition,
                                _siteConfig.Watermarkimgquality, _siteConfig.Watermarkfont, _siteConfig.Watermarkfontsize);
                            break;

                        case 2:
                            WaterMark.AddImageSignPic(newFilePath, newFilePath,
                                _siteConfig.Watermarkpic, _siteConfig.Watermarkposition,
                                _siteConfig.Watermarkimgquality, _siteConfig.Watermarktransparency);
                            break;
                    }
                }
                if (!isSaveFullPath)
                    return "{\"status\": 1, \"msg\": \"上传文件成功！\", \"name\": \""
                           + fileName + "\", \"path\": \"" + newFilePath + "\", \"thumb\": \""
                           + newThumbnailPath + "\", \"size\": " + fileSize + ", \"ext\": \"" + fileExt + "\"}";

                newFilePath = "http://" + HttpContext.Request.ServerVariables["HTTP_HOST"] + newFilePath;
                newThumbnailPath = "http://" + HttpContext.Request.ServerVariables["HTTP_HOST"] + newThumbnailPath;
                //处理完毕，返回JOSN格式的文件信息
                return "{\"status\": 1, \"msg\": \"上传文件成功！\", \"name\": \""
                    + fileName + "\", \"path\": \"" + newFilePath + "\", \"thumb\": \""
                    + newThumbnailPath + "\", \"size\": " + fileSize + ", \"ext\": \"" + fileExt + "\"}";
            }
            catch
            {
                return "{\"status\": 0, \"msg\": \"上传过程中发生意外错误！\"}";
            }
        }

        #region 辅助方法

        /// <summary>
        /// 是否为图片文件
        /// </summary>
        /// <param name="fileExt">文件扩展名，不含“.”</param>
        private bool IsImage(string fileExt)
        {
            var al = new ArrayList { "bmp", "jpeg", "jpg", "gif", "png" };
            return al.Contains(fileExt.ToLower());
        }

        /// <summary>
        /// 是否需要打水印
        /// </summary>
        /// <param name="fileExt">文件扩展名，不含“.”</param>
        private bool IsWaterMark(string fileExt)
        {
            //判断是否开启水印
            if (_siteConfig.Watermarktype <= 0) return false;
            //判断是否可以打水印的图片类型
            var al = new ArrayList { "bmp", "jpeg", "jpg", "png" };
            return al.Contains(fileExt.ToLower());
        }

        /// <summary>
        /// 返回上传目录相对路径
        /// </summary>
        private string GetUpLoadPath()
        {
            string path = _siteConfig.Webpath + _siteConfig.Filepath + "/"; //站点目录+上传目录
            switch (_siteConfig.Filesave)
            {
                case 1: //按年月日每天一个文件夹
                    path += DateTime.Now.ToString("yyyyMMdd");
                    break;

                default: //按年月/日存入不同的文件夹
                    path += DateTime.Now.ToString("yyyyMM") + "/" + DateTime.Now.ToString("dd");
                    break;
            }
            return path + "/";
        }

        /// <summary>
        /// 检查文件大小是否合法
        /// </summary>
        /// <param name="fileExt">文件扩展名，不含“.”</param>
        /// <param name="fileSize">文件大小(B)</param>
        private bool CheckFileSize(string fileExt, int fileSize)
        {
            //判断是否为图片文件
            if (IsImage(fileExt))
            {
                if (_siteConfig.Imgsize > 0 && fileSize > _siteConfig.Imgsize * 1024)
                {
                    return false;
                }
            }
            else
            {
                if (_siteConfig.Attachsize > 0 && fileSize > _siteConfig.Attachsize * 1024)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 检查是否为合法的上传文件
        /// </summary>
        private bool CheckFileExt(string fileExt)
        {
            //检查危险文件
            string[] excExt = { "asp", "aspx", "php", "jsp", "htm", "html" };
            if (excExt.Any(t => String.Equals(t, fileExt, StringComparison.CurrentCultureIgnoreCase)))
            {
                return false;
            }
            //检查合法文件
            var allowExt = _siteConfig.Fileextension.Split(',');
            return allowExt.Any(t => String.Equals(t, fileExt, StringComparison.CurrentCultureIgnoreCase));
        }

        #endregion 辅助方法
    }
}