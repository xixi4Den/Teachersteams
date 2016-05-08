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
using DataAssignment = Teachersteams.Domain.Entities.Assignment;
using DataStudent = Teachersteams.Domain.Entities.Student;

namespace Teachersteams.Business.Retrievers.Board.Student
{
    [StudentBoardItemsRetrieverMeta(StudentBoardFilterType.Checked)]
    public class CheckedStudentBoardItemsRetriever : IStudentBoardItemsRetriever
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IGridOptionsHelper gridOptionsHelper;

        public CheckedStudentBoardItemsRetriever(IUnitOfWork unitOfWork,
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
                FilterRules = x => groupIds.Contains(x.GroupId) && x.Results.Any(r => r.Student.Uid == studentUid && r.Grade.HasValue),
                PageRules = new PageSettings(gridOptions.PageNumber, gridOptions.PageSize),
                SortRules = gridOptionsHelper.BuidDynamicOrderedQuery<DataAssignment>(gridOptions)
            }).ToList();

            var boardItems = mapper.MapManyTo<StudentBoardItemViewModel>(assignments).ToList();

            foreach (var item in boardItems)
            {
                var entity = assignments.Single(x => x.Id == item.AssignmentId);
                var result = entity.Results.SingleOrDefault(x => x.Student.Uid == studentUid);
                if (result != null)
                {
                    item.AssignmentResultId = result.Id;
                    item.CompletionDate = result.CompletionDate;
                    item.AssignmentResultFile = result.File;
                    item.AssigneeTeacherUid = result.AssigneeTeacher != null ? result.AssigneeTeacher.Uid : null;
                    item.Grade = result.Grade;
                    item.CheckDate = result.CheckDate;
                }
            }

            return boardItems;
        }

        public int Count(string studentUid)
        {
            var groupIds = GetGroupIds(studentUid);

            return unitOfWork.Count(new QueryParameters<DataAssignment>
            {
                FilterRules = x => groupIds.Contains(x.GroupId) && x.Results.Any(r => r.Student.Uid == studentUid && r.Grade.HasValue)
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
