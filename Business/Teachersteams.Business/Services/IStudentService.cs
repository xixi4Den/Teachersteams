using System;
using System.Collections.Generic;
using Teachersteams.Business.ViewModels.Grid;
using Teachersteams.Business.ViewModels.User;
using Teachersteams.Shared.Dependency;

namespace Teachersteams.Business.Services
{
    public interface IStudentService: IRequestDependency
    {
        StudentViewModel Invite(StudentViewModel viewModel);

        IEnumerable<StudentViewModel> GetUsers(Guid groupId, GridOptions gridOptions);

        int Count(Guid groupId);
    }
}
