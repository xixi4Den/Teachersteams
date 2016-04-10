using Teachersteams.Business.ViewModels.User;
using Teachersteams.Shared.Dependency;

namespace Teachersteams.Business.Services
{
    public interface IUserService: IRequestDependency
    {
        TeacherViewModel InviteTeacher(TeacherViewModel viewModel);
    }
}
