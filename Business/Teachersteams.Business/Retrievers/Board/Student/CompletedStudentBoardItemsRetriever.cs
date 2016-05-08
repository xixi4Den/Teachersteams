using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Teachersteams.Business.Attributes;
using Teachersteams.Business.Enums;
using Teachersteams.Business.Helpers;
using Teachersteams.Business.ViewModels.Board;
using Teachersteams.Domain;
using DataAssignment = Teachersteams.Domain.Entities.Assignment;
using DataStudent = Teachersteams.Domain.Entities.Student;

namespace Teachersteams.Business.Retrievers.Board.Student
{
    [StudentBoardItemsRetrieverMeta(StudentBoardFilterType.Completed)]
    public class CompletedStudentBoardItemsRetriever : BaseStudentBoardItemsRetriever
    {
        public CompletedStudentBoardItemsRetriever(IUnitOfWork unitOfWork,
            IMapper mapper,
            IGridOptionsHelper gridOptionsHelper): base(unitOfWork, mapper, gridOptionsHelper)
        {
        }

        protected override Expression<Func<DataAssignment, bool>> GetFilterExpression(string studentUid, IEnumerable<Guid> groupIds)
        {
            return x => groupIds.Contains(x.GroupId) 
                && x.Results.Any(r => r.Student.Uid == studentUid && !r.Grade.HasValue);
        }

        protected override void FillAdditionalFields(string studentUid, List<StudentBoardItemViewModel> boardItems, List<DataAssignment> assignments)
        {
            base.FillAdditionalFields(studentUid, boardItems, assignments);

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
                }
            }
        }
    }
}
