using System.Collections.Generic;
using Teachersteams.Business.Enums;
using Teachersteams.Business.ViewModels.Board;
using Teachersteams.Business.ViewModels.Grid;
using Teachersteams.Shared.Dependency;

namespace Teachersteams.Business.Services.Contracts
{
    public interface ITeacherBoardItemsProvider: IRequestDependency
    {
        IEnumerable<TeacherBoardItemViewModel> GetAssignments(string teacherUid, TeacherBoardCheckFilterType? checkFilterType, TeacherBoardAssignFilterType assignFilterType, GridOptions gridOptions);
        int AssignmentsCount(string teacherUid, TeacherBoardCheckFilterType? checkFilterType, TeacherBoardAssignFilterType assignFilterType);
    }
}
