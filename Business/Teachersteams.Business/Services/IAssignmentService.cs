using Teachersteams.Business.ViewModels.Assignment;
using Teachersteams.Shared.Dependency;

namespace Teachersteams.Business.Services
{
    public interface IAssignmentService: IRequestDependency
    {
        AssignmentViewModel Create(string uid, AssignmentViewModel viewModel);
    }
}
