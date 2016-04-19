using System.Reflection;
using Autofac;
using AutoMapper;
using AutoMapper.Internal;
using Teachersteams.Business.Attributes;
using Teachersteams.Business.Retrievers.Group;
using Teachersteams.Business.Retrievers.Group.Contract;
using Teachersteams.Business.Utils;
using Teachersteams.Shared.Dependency;
using Module = Autofac.Module;

namespace Teachersteams.Business.Modules
{
    public class BusinessModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterAutoMapper(builder);
            builder.RegisterDependencies(Assembly.GetExecutingAssembly());
            RegisterGroupRetrievers(builder);

            base.Load(builder);
        }

        private void RegisterAutoMapper(ContainerBuilder builder)
        {
            var profiles = MapperUtils.GetAllProfiles();

            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                profiles.Each(cfg.AddProfile);
            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper())
                .As<IMapper>();
        }

        private void RegisterGroupRetrievers(ContainerBuilder builder)
        {
            RegisterGroupRetriever<AllTeacherGroupRetriever>(builder);
            RegisterGroupRetriever<OwnGroupRetriever>(builder);
            RegisterGroupRetriever<AssistantGroupRetriever>(builder);
            RegisterGroupRetriever<AllStudentGroupRetriever>(builder);
        }

        private void RegisterGroupRetriever<T>(ContainerBuilder builder)
        {
            var filterType = typeof (T).GetCustomAttribute<GroupRetrieverMetaAttribute>().FilterType;
            builder.RegisterType<T>()
                .Keyed<IGroupRetriever>(filterType)
                .InstancePerLifetimeScope();
        }
    }
}
