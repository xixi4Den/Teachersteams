using System.Collections.Generic;
using Teachersteams.Business.ViewModels.Board;
using Teachersteams.Business.ViewModels.Grid;

namespace Teachersteams.Business.Retrievers.Board.Teacher
{
    public interface ITeacherBoardItemsRetriever
    {
        IEnumerable<TeacherBoardItemViewModel> Retrieve(string teacherUid, GridOptions gridOptions);

        int Count(string teacherUid);
    }
}
