using System;
using Autofac;
using EFDTO.Entity;
using System.Web.Mvc;
using WorkData.Dto.Entity;
using WorkData.BLL.Interface;

namespace WorkData.Web.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
    }
}