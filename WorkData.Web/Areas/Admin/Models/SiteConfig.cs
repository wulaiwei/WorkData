// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Web 
// 文件名：SiteConfig.cs
// 创建标识：吴来伟 2016-11-07
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------
namespace WorkData.Web.Areas.Admin.Models
{
    /// <summary>
    ///     文件上传配置类
    /// </summary>
    public class SiteConfig
    {
        #region 文件上传设置==================================

        /// <summary>
        ///     附件上传目录
        /// </summary>
        public string Filepath { get; set; } = "Upload";

        /// <summary>
        ///     网站路径
        /// </summary>
        public string Webpath { get; set; } = "/";

        /// <summary>
        ///     附件保存方式
        /// </summary>
        public int Filesave { get; set; } = 1;

        /// <summary>
        ///     附件上传类型
        /// </summary>
        public string Fileextension { get; set; } = "gif,jpg,jpge,png,gif,jpeg,bmp,rar,zip,doc,xls,txt,pdf,xlsx,docx,ppt,pptx";

        /// <summary>
        ///     文件上传大小
        /// </summary>
        public int Attachsize { get; set; } = 51200;

        /// <summary>
        ///     图片上传大小
        /// </summary>
        public int Imgsize { get; set; } = 204800;

        /// <summary>
        ///     图片最大高度(像素)
        /// </summary>
        public int Imgmaxheight { get; set; } = 800;

        /// <summary>
        ///     图片最大宽度(像素)
        /// </summary>
        public int Imgmaxwidth { get; set; } = 800;

        /// <summary>
        ///     生成缩略图高度(像素)
        /// </summary>
        public int Thumbnailheight { get; set; } = 300;

        /// <summary>
        ///     生成缩略图宽度(像素)
        /// </summary>
        public int Thumbnailwidth { get; set; } = 300;

        /// <summary>
        ///     图片水印类型
        /// </summary>
        public int Watermarktype { get; set; } = 2;

        /// <summary>
        ///     图片水印位置
        /// </summary>
        public int Watermarkposition { get; set; } = 7;

        /// <summary>
        ///     图片生成质量
        /// </summary>
        public int Watermarkimgquality { get; set; } = 80;

        /// <summary>
        ///     图片水印文件
        /// </summary>
        public string Watermarkpic { get; set; } = "/images/logo2.png";

        /// <summary>
        ///     水印透明度
        /// </summary>
        public int Watermarktransparency { get; set; } = 6;

        /// <summary>
        ///     水印文字
        /// </summary>
        public string Watermarktext { get; set; } = "华亨装修网";

        /// <summary>
        ///     文字字体
        /// </summary>
        public string Watermarkfont { get; set; } = "Tahoma";

        /// <summary>
        ///     文字大小(像素)
        /// </summary>
        public int Watermarkfontsize { get; set; } = 12;

        #endregion 文件上传设置==================================
    }
}