using System.Reflection;
using Autofac;
using AutoMapper;
using AutoMapper.Internal;
using Teachersteams.Business.Attributes;
using Teachersteams.Business.Retrievers.Assignment;
using Teachersteams.Business.Retrievers.Board.Student;
using Teachersteams.Business.Retrievers.Board.Teacher;
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
            RegisterAssignmentRetrievers(builder);
            RegisterStudentBoardItemsRetrievers(builder);
            RegisterTeacherBoardItemsRetrievers(builder);

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

        private void RegisterAssignmentRetrievers(ContainerBuilder builder)
        {
            RegisterAssignmentRetriever<AssignmentRetrieverForStudent>(builder);
            RegisterAssignmentRetriever<AssignmentRetrieverForTeacher>(builder);
        }

        private void RegisterStudentBoardItemsRetrievers(ContainerBuilder builder)
        {
            RegisterStudentBoardItemsRetriever<NewStudentBoardItemsRetriever>(builder);
            RegisterStudentBoardItemsRetriever<ExpiredStudentBoardItemsRetriever>(builder);
            RegisterStudentBoardItemsRetriever<CompletedStudentBoardItemsRetriever>(builder);
            RegisterStudentBoardItemsRetriever<CheckedStudentBoardItemsRetriever>(builder);
        }

        private void RegisterTeacherBoardItemsRetrievers(ContainerBuilder builder)
        {
            RegisterTeacherBoardItemsRetriever<NotAssignedTeacherBoardItemsRetriever>(builder);
            RegisterTeacherBoardItemsRetriever<UncheckedAssignedToMeTeacherBoardItemsRetriever>(builder);
            RegisterTeacherBoardItemsRetriever<CheckedAssignedToMeTeacherBoardItemsRetriever>(builder);
            RegisterTeacherBoardItemsRetriever<UncheckedAssignedToOthersTeacherBoardItemsRetriever>(builder);
            RegisterTeacherBoardItemsRetriever<CheckedAssignedToOthersTeacherBoardItemsRetriever>(builder);
        }

        private void RegisterGroupRetriever<T>(ContainerBuilder builder) where T: IGroupRetriever
        {
            var filterType = typeof (T).GetCustomAttribute<GroupRetrieverMetaAttribute>().FilterType;
            builder.RegisterType<T>()
                .Keyed<IGroupRetriever>(filterType)
                .InstancePerLifetimeScope();
        }

        private void RegisterAssignmentRetriever<T>(ContainerBuilder builder) where T: IAssignmentRetriever
        {
            var userType = typeof(T).GetCustomAttribute<UserTypeSpecificRetrieverMetaAttribute>().UserType;
            builder.RegisterType<T>()
                .Keyed<IAssignmentRetriever>(userType)
                .InstancePerLifetimeScope();
        }

        private void RegisterStudentBoardItemsRetriever<T>(ContainerBuilder builder) where T : IStudentBoardItemsRetriever
        {
            var userType = typeof(T).GetCustomAttribute<StudentBoardItemsRetrieverMetaAttribute>().FilterType;
            builder.RegisterType<T>()
                .Keyed<IStudentBoardItemsRetriever>(userType)
                .InstancePerLifetimeScope();
        }

        private void RegisterTeacherBoardItemsRetriever<T>(ContainerBuilder builder) where T : ITeacherBoardItemsRetriever
        {
            var userType = typeof(T).GetCustomAttribute<TeacherBoardItemsRetrieverMetaAttribute>().FilterType;
            builder.RegisterType<T>()
                .Keyed<ITeacherBoardItemsRetriever>(userType)
                .InstancePerLifetimeScope();
        }
    }
}
