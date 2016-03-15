using System.Reflection;
using Autofac;

namespace Teachersteams.Shared.Dependency
{
    public static class ContainerBuilderExtensions
    {
        /// <summary>
        /// Register dependencies marked with marker interface
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="assemblies"></param>
        public static void RegisterDependencies(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                RegisterPerDependency(builder, assembly);
                RegisterSingle(builder, assembly);
                RegisterPerRequest(builder, assembly);
            }   
        }

        private static void RegisterPerRequest(ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                .AsImplementedInterfaces()
                .AssignableTo<IRequestDependency>()
                .InstancePerRequest();
        }

        private static void RegisterSingle(ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                .AsImplementedInterfaces()
                .AssignableTo<ISingletonDependency>()
                .SingleInstance();
        }

        private static void RegisterPerDependency(ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                .AsImplementedInterfaces()
                .AssignableTo<IDependency>()
                .InstancePerDependency();
        }
    }
}
