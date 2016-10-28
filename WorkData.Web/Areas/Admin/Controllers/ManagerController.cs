using System.Web.Mvc;

namespace WorkData.Web.Areas.Admin.Controllers
{
    public class ManagerController : Controller
    {
        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 默认页
        /// </summary>
        /// <returns></returns>
        public ActionResult Center()
        {
            return View();
        }
    }
}