// ------------------------------------------------------------------------------
// Copyright  成都联宇创新科技有限公司 版权所有。 
// 项目名：WorkData.Web 
// 文件名：BundleConfig.cs
// 创建标识：吴来伟 2016-11-24
// 创建描述：
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------
using System.Web.Optimization;

namespace WorkData.Web
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        { 
            //Jquery必备的StyleBundle和ScriptBundle
            var css = new StyleBundle("~/Areas/Admin/skin/default/style.css")
                        .Include("~/Areas/Admin/skin/common.css",
                        "~/Css/pagination.css"
                        , "~/scripts/artdialog/ui-dialog.css");

            var jquery = new ScriptBundle("~/scripts/jquery/jquery-1.11.2.min.js")
                        .Include("~/scripts/jquery/Validform_v5.3.2_min.js", 
                        "~/scripts/jquery/jquery.nicescroll.js"
                        , "~/scripts/artdialog/dialog-plus-min.js");

            //公用js
            jquery.Include("~/Areas/Admin/js/layindex.js",
                    "~/Areas/Admin/js/laymain.js",
                    "~/Areas/Admin/js/common.js");

            //上传js
            var uploadJquery = new ScriptBundle("~/scripts/webuploader/webuploader.min.js")
                    .Include("~/Areas/Admin/js/uploader.js");

            //date
            var date = new ScriptBundle("~/scripts/datepicker/WdatePicker.js");

            //angularjs
            var ng = new ScriptBundle("~/scripts/angular.min.js");

            bundles.Add(css);
            bundles.Add(jquery);
            bundles.Add(uploadJquery);
            bundles.Add(date);
            bundles.Add(ng);
        }
    }
}