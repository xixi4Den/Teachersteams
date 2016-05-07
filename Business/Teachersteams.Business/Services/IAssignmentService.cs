using System;
using System.Collections.Generic;
using Teachersteams.Business.Enums;
using Teachersteams.Business.ViewModels.Assignment;
using Teachersteams.Business.ViewModels.Grid;
using Teachersteams.Shared.Dependency;

namespace Teachersteams.Business.Services
{
    public interface IAssignmentService: IRequestDependency
    {
        AssignmentViewModel CreateAssignment(string uid, AssignmentViewModel viewModel);

        IEnumerable<AssignmentViewModel> GetAllAssignments(Guid groupId, UserType userType, string uid, GridOptions gridOptions);

        int AssignmentCount(Guid groupId);

        void CompleteAssignment(string uid, AssignmentCompletionViewModel viewModel);

        IEnumerable<AssignmentResultViewModel> GetAssignmentResults(Guid assignmentId, GridOptions gridOptions);

        int ResultCount(Guid assignmentId);

        void AssignResult(Guid assignmentResultId, string teacherUid);

        void GradeAssignmentResult(Guid assignmnetResultId, byte grade, string teacherUid);
    }
}
