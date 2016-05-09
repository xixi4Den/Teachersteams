using System.Collections.Generic;
using Autofac.Features.Indexed;
using Teachersteams.Business.Enums;
using Teachersteams.Business.Retrievers.Board.Teacher;
using Teachersteams.Business.Services.Contracts;
using Teachersteams.Business.Utils;
using Teachersteams.Business.ViewModels.Board;
using Teachersteams.Business.ViewModels.Grid;

namespace Teachersteams.Business.Services
{
    public class TeacherBoardItemsProvider: ITeacherBoardItemsProvider
    {
        private readonly IIndex<TeacherBoardCompositeFilterType, ITeacherBoardItemsRetriever> retrievers;

        public TeacherBoardItemsProvider(IIndex<TeacherBoardCompositeFilterType, ITeacherBoardItemsRetriever> retrievers)
        {
            this.retrievers = retrievers;
        }

        public IEnumerable<TeacherBoardItemViewModel> GetAssignments(string teacherUid, TeacherBoardCheckFilterType? checkFilterType,
            TeacherBoardAssignFilterType assignFilterType, GridOptions gridOptions)
        {
            var compositeFilter = EnumUtils.GetTeacherBoardCompositeFilterType(checkFilterType, assignFilterType);
            return retrievers[compositeFilter].Retrieve(teacherUid, gridOptions);
        }

        public int AssignmentsCount(string teacherUid, TeacherBoardCheckFilterType? checkFilterType,
            TeacherBoardAssignFilterType assignFilterType)
        {
            var compositeFilter = EnumUtils.GetTeacherBoardCompositeFilterType(checkFilterType, assignFilterType);
            return retrievers[compositeFilter].Count(teacherUid);
        }
    }
}
