using Autofac;
using Teachersteams.DataAccess;
using Teachersteams.Domain;

namespace Teachersteams.Business.Modules
{
    public class DataAccessModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Context>().AsSelf().AsImplementedInterfaces();
            RegisterRepository(builder);
            RegisterUnitOfWork(builder);
            base.Load(builder);
        }

        private static void RegisterRepository(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof (Repository<>)).As(typeof (IRepository<>));
        }

        private static void RegisterUnitOfWork(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
        }
    }
}
