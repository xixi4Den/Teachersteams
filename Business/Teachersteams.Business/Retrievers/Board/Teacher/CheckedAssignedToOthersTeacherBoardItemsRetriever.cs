using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Teachersteams.Business.Attributes;
using Teachersteams.Business.Enums;
using Teachersteams.Business.Helpers;
using Teachersteams.Domain;
using Teachersteams.Domain.Entities;

namespace Teachersteams.Business.Retrievers.Board.Teacher
{
    [TeacherBoardItemsRetrieverMeta(TeacherBoardCompositeFilterType.CheckedAssignedToOthers)]
    public class CheckedAssignedToOthersTeacherBoardItemsRetriever : BaseTeacherBoardItemsRetriever
    {
        public CheckedAssignedToOthersTeacherBoardItemsRetriever(IUnitOfWork unitOfWork, IMapper mapper, IGridOptionsHelper gridOptionsHelper) 
            : base(unitOfWork, mapper, gridOptionsHelper)
        {
        }

        protected override Expression<Func<AssignmentResult, bool>> GetFilterCondition(IEnumerable<Guid> groupIds, string teacherUid)
        {
            return x => groupIds.Contains(x.Assignment.GroupId)
                && (x.AssigneeTeacherId.HasValue && x.AssigneeTeacher.Uid != teacherUid)
                && x.Grade.HasValue;
        }
    }
}
