using Autofac.Integration.Mvc;
using System.Web.Mvc;
using System.Web.Routing;
using WorkData.Dto;

namespace WorkData.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Bootstrap.Initiate();
            //集成AUTOFAC MVC
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Bootstrap.ApplicationContainer));
            AutoMapperConfiguration.Configure();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}