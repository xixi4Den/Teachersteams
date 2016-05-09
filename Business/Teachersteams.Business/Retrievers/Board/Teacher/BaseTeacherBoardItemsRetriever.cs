using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Teachersteams.Business.Extensions;
using Teachersteams.Business.Helpers;
using Teachersteams.Business.ViewModels.Board;
using Teachersteams.Business.ViewModels.Grid;
using Teachersteams.Domain;
using Teachersteams.Domain.Entities;
using Teachersteams.Domain.Query;
using DataTeacher = Teachersteams.Domain.Entities.Teacher;

namespace Teachersteams.Business.Retrievers.Board.Teacher
{
    public abstract class BaseTeacherBoardItemsRetriever: ITeacherBoardItemsRetriever
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IGridOptionsHelper gridOptionsHelper;

        public BaseTeacherBoardItemsRetriever(IUnitOfWork unitOfWork, IMapper mapper, IGridOptionsHelper gridOptionsHelper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.gridOptionsHelper = gridOptionsHelper;
        }

        public IEnumerable<TeacherBoardItemViewModel> Retrieve(string teacherUid, GridOptions gridOptions)
        {
            var groupIds = GetGroupIds(teacherUid);
            var assignmentResults = unitOfWork.GetAll(new QueryParameters<AssignmentResult>
            {
                FilterRules = GetFilterCondition(groupIds, teacherUid),
                PageRules = new PageSettings(gridOptions.PageNumber, gridOptions.PageSize),
                SortRules = gridOptionsHelper.BuidDynamicOrderedQuery<AssignmentResult>(gridOptions)
            });

            return mapper.MapManyTo<TeacherBoardItemViewModel>(assignmentResults);
        }

        public int Count(string teacherUid)
        {
            var groupIds = GetGroupIds(teacherUid);
            return unitOfWork.Count(new QueryParameters<AssignmentResult>
            {
                FilterRules = GetFilterCondition(groupIds, teacherUid)
            });
        }

        protected abstract Expression<Func<AssignmentResult, bool>> GetFilterCondition(IEnumerable<Guid> groupIds, string teacherUid);

        private IEnumerable<Guid> GetGroupIds(string teacherUid)
        {
            return unitOfWork.GetAll(new QueryParameters<DataTeacher>
            {
                FilterRules = x => x.Uid == teacherUid
            }).Select(x => x.GroupId);
        }
    }
}
