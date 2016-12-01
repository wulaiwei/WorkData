// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Web 
// 文件名：Api.cs
// 创建标识：吴来伟 2016-11-25
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

using System.Web;

namespace WorkData.Util
{
    public static class Api
    {
        public static string Root = "http://" + HttpContext.Current.Request.Url.Authority;
        public static string PhysicsUrl = HttpContext.Current.Server.MapPath("~");
        public static string Content = Root + "/Content";
        public static string Upload = Root + "/UpLoad";
        public static string Scripts = Root + "/Scripts";


    }
}