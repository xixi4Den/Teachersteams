using System.Collections.Generic;
using Teachersteams.Business.ViewModels.Board;
using Teachersteams.Business.ViewModels.Grid;

namespace Teachersteams.Business.Retrievers.Board.Student
{
    public interface IStudentBoardItemsRetriever
    {
        IEnumerable<StudentBoardItemViewModel> Retrieve(string studentUid, GridOptions gridOptions);

        int Count(string studentUid);
    }
}
