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
using Teachersteams.Domain.Query;
using AssignmentStatus = Teachersteams.Domain.Enums.AssignmentStatus;
using DataAssignment = Teachersteams.Domain.Entities.Assignment;
using DataStudent = Teachersteams.Domain.Entities.Student;

namespace Teachersteams.Business.Retrievers.Board.Student
{
    public abstract class BaseStudentBoardItemsRetriever: IStudentBoardItemsRetriever
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IGridOptionsHelper gridOptionsHelper;

        public BaseStudentBoardItemsRetriever(IUnitOfWork unitOfWork,
            IMapper mapper,
            IGridOptionsHelper gridOptionsHelper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.gridOptionsHelper = gridOptionsHelper;
        }

        public IEnumerable<StudentBoardItemViewModel> Retrieve(string studentUid, GridOptions gridOptions)
        {
            var groupIds = GetGroupIds(studentUid);

            var assignments = unitOfWork.GetAll(new QueryParameters<DataAssignment>
            {
                FilterRules = GetFilterExpression(studentUid, groupIds),
                PageRules = new PageSettings(gridOptions.PageNumber, gridOptions.PageSize),
                SortRules = gridOptionsHelper.BuidDynamicOrderedQuery<DataAssignment>(gridOptions)
            }).ToList();

            var boardItems = mapper.MapManyTo<StudentBoardItemViewModel>(assignments).ToList();

            FillAdditionalFields(studentUid, boardItems, assignments);

            return boardItems;
        }

        public int Count(string studentUid)
        {
            var groupIds = GetGroupIds(studentUid);

            return unitOfWork.Count(new QueryParameters<DataAssignment>
            {
                FilterRules = x => groupIds.Contains(x.GroupId) && x.Results.All(r => r.Student.Uid != studentUid) && (x.Status == AssignmentStatus.Active && x.ExpirationDate > DateTime.UtcNow)
            });
        }

        protected abstract Expression<Func<DataAssignment, bool>> GetFilterExpression(string studentUid, IEnumerable<Guid> groupIds);

        protected virtual void FillAdditionalFields(string studentUid, List<StudentBoardItemViewModel> boardItems, List<DataAssignment> assignments)
        {
            
        }

        protected IEnumerable<Guid> GetGroupIds(string studentUid)
        {
            return unitOfWork.GetAll(new QueryParameters<DataStudent>
            {
                FilterRules = x => x.Uid == studentUid
            }).Select(x => x.GroupId);
        }
    }
}
