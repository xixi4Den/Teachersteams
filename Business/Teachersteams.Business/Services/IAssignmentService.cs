using System;
using System.Collections.Generic;
using Teachersteams.Business.ViewModels.Assignment;
using Teachersteams.Business.ViewModels.Grid;
using Teachersteams.Shared.Dependency;

namespace Teachersteams.Business.Services
{
    public interface IAssignmentService: IRequestDependency
    {
        AssignmentViewModel Create(string uid, AssignmentViewModel viewModel);

        IEnumerable<AssignmentViewModel> GetAll(Guid groupId, GridOptions gridOptions);

        int Count(Guid groupId);
    }
}
