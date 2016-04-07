using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Teachersteams.Api.Filters;

namespace Teachersteams.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            //GlobalConfiguration.Configuration.Filters.Add(new ApiPermissionsAttribute());
            GlobalConfiguration.Configuration.Filters.Add(new ExceptionHandlingAttribute());
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            ContainerConfiguration.Configure();
        }
    }
}
