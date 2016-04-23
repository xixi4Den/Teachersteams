using System;
using System.Collections.Generic;
using Teachersteams.Business.ViewModels;
using Teachersteams.Business.ViewModels.Grid;
using Teachersteams.Business.ViewModels.User;
using Teachersteams.Shared.Dependency;

namespace Teachersteams.Business.Services
{
    public interface ITeacherService: IRequestDependency
    {
        TeacherViewModel Invite(TeacherViewModel viewModel);

        IEnumerable<TeacherViewModel> GetUsers(Guid groupId, GridOptions gridOptions);

        int Count(Guid groupId);

        IEnumerable<RequestViewModel> GetRequests(string uid);

        void Response(RequestViewModel viewModel);

        bool AnyRequest(string uid, Guid groupId);

        int RequestsCount(string uid);
    }
}
