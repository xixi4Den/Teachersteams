using System.Collections.Generic;
using Teachersteams.Business.Enums;
using Teachersteams.Business.ViewModels.Board;
using Teachersteams.Business.ViewModels.Grid;
using Teachersteams.Shared.Dependency;

namespace Teachersteams.Business.Services.Contracts
{
    public interface IStudentBoardItemsProvider : IRequestDependency
    {
        IEnumerable<StudentBoardItemViewModel> GetAssignments(string studentUid, StudentBoardFilterType filterType, GridOptions gridOptions);
        int AssignmentsCount(string userId, StudentBoardFilterType filterType);
    }
}
