using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Teachersteams.Business.Attributes;
using Teachersteams.Business.Enums;
using Teachersteams.Business.Helpers;
using Teachersteams.Domain;
using AssignmentStatus = Teachersteams.Domain.Enums.AssignmentStatus;
using DataAssignment = Teachersteams.Domain.Entities.Assignment;
using DataStudent = Teachersteams.Domain.Entities.Student;

namespace Teachersteams.Business.Retrievers.Board.Student
{
    [StudentBoardItemsRetrieverMeta(StudentBoardFilterType.Expired)]
    public class ExpiredStudentBoardItemsRetriever : BaseStudentBoardItemsRetriever
    {
        public ExpiredStudentBoardItemsRetriever(IUnitOfWork unitOfWork,
            IMapper mapper,
            IGridOptionsHelper gridOptionsHelper): base(unitOfWork, mapper, gridOptionsHelper)
        {
        }

        protected override Expression<Func<DataAssignment, bool>> GetFilterExpression(string studentUid, IEnumerable<Guid> groupIds)
        {
            return x => groupIds.Contains(x.GroupId) 
                && x.Results.All(r => r.Student.Uid != studentUid) 
                && (x.Status == AssignmentStatus.Expired || x.ExpirationDate <= DateTime.UtcNow);
        }
    }
}
