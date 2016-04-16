using System;
using System.Collections.Generic;
using Teachersteams.Business.ViewModels.Grid;
using Teachersteams.Business.ViewModels.User;
using Teachersteams.Shared.Dependency;

namespace Teachersteams.Business.Services
{
    public interface IUserService: IRequestDependency
    {
        TeacherViewModel InviteTeacher(TeacherViewModel viewModel);

        IEnumerable<TeacherViewModel> GetTeachers(Guid groupId, GridOptions gridOptions);

        int Count(Guid groupId);
    }
}
