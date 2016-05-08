using System.Collections.Generic;
using Autofac.Features.Indexed;
using Teachersteams.Business.Enums;
using Teachersteams.Business.Retrievers.Board.Student;
using Teachersteams.Business.Services.Contracts;
using Teachersteams.Business.ViewModels.Board;
using Teachersteams.Business.ViewModels.Grid;

namespace Teachersteams.Business.Services
{
    public class StudentBoardItemsProvider: IStudentBoardItemsProvider
    {
        private readonly IIndex<StudentBoardFilterType, IStudentBoardItemsRetriever> retrievers;

        public StudentBoardItemsProvider(IIndex<StudentBoardFilterType, IStudentBoardItemsRetriever> retrievers)
        {
            this.retrievers = retrievers;
        }

        public IEnumerable<StudentBoardItemViewModel> GetAssignments(string studentUid, StudentBoardFilterType filterType,
            GridOptions gridOptions)
        {
            return retrievers[filterType].Retrieve(studentUid, gridOptions);
        }

        public int AssignmentsCount(string userId, StudentBoardFilterType filterType)
        {
            return retrievers[filterType].Count(userId);
        }
    }
}
