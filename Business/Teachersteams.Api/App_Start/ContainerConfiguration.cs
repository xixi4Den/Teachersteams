using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Teachersteams.Business.Modules;
using Teachersteams.Business.Services;
using Teachersteams.Shared.Dependency;

namespace Teachersteams.Api
{
    public static class ContainerConfiguration
    {
        public static Assembly ApiAssembly
        {
            get
            {
                return Assembly.GetExecutingAssembly();
            }
        } 

        public static void Configure()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(ApiAssembly);
            builder.RegisterWebApiFilterProvider(config);
            builder.RegisterModule<DataAccessModule>();
            builder.RegisterDependencies(ApiAssembly);
            builder.RegisterDependencies(typeof(ITestService).Assembly);
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}