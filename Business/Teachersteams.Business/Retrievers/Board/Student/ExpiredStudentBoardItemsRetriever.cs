using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Teachersteams.Business.Attributes;
using Teachersteams.Business.Enums;
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
    [StudentBoardItemsRetrieverMeta(StudentBoardFilterType.Expired)]
    public class ExpiredStudentBoardItemsRetriever: IStudentBoardItemsRetriever
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IGridOptionsHelper gridOptionsHelper;

        public ExpiredStudentBoardItemsRetriever(IUnitOfWork unitOfWork,
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
                FilterRules = x => groupIds.Contains(x.GroupId) && x.Results.All(r => r.Student.Uid != studentUid) && (x.Status == AssignmentStatus.Expired || x.ExpirationDate <= DateTime.UtcNow),
                PageRules = new PageSettings(gridOptions.PageNumber, gridOptions.PageSize),
                SortRules = gridOptionsHelper.BuidDynamicOrderedQuery<DataAssignment>(gridOptions)
            }).ToList();

            var boardItems = mapper.MapManyTo<StudentBoardItemViewModel>(assignments).ToList();

            return boardItems;
        }

        public int Count(string studentUid)
        {
            var groupIds = GetGroupIds(studentUid);

            return unitOfWork.Count(new QueryParameters<DataAssignment>
            {
                FilterRules = x => groupIds.Contains(x.GroupId) && x.Results.All(r => r.Student.Uid != studentUid) && x.Status == AssignmentStatus.Expired
            });
        }

        private IEnumerable<Guid> GetGroupIds(string studentUid)
        {
            return unitOfWork.GetAll(new QueryParameters<DataStudent>
            {
                FilterRules = x => x.Uid == studentUid
            }).Select(x => x.GroupId);
        }
    }
}
