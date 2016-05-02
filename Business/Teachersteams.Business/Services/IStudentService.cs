using System;
using System.Collections.Generic;
using Teachersteams.Business.Enums;
using Teachersteams.Business.ViewModels;
using Teachersteams.Business.ViewModels.Grid;
using Teachersteams.Business.ViewModels.User;
using Teachersteams.Shared.Dependency;

namespace Teachersteams.Business.Services
{
    public interface IStudentService: IRequestDependency
    {
        StudentViewModel Invite(StudentViewModel viewModel);

        IEnumerable<StudentViewModel> GetUsers(Guid groupId, GridOptions gridOptions, UserType userType);

        int Count(Guid groupId, UserType userType);

        IEnumerable<RequestViewModel> GetRequests(string uid);

        void Response(RequestViewModel viewModel);

        bool AnyRequest(string uid, Guid groupId);

        int RequestsCount(string uid);

        void Delete(string uid, Guid groupId);
    }
}
