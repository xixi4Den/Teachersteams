using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Teachersteams.Business.Enums;
using Teachersteams.Domain;
using Teachersteams.Business.Attributes;
using Teachersteams.Domain.Entities;
using Teachersteams.Domain.Query;
using DataGroup = Teachersteams.Domain.Entities.Group;
using UserStatus = Teachersteams.Domain.Enums.UserStatus;

namespace Teachersteams.Business.Retrievers.Group
{

    [GroupRetrieverMeta(GroupFilterType.AllForTeacher)]
    public class AllTeacherGroupRetriever : BaseGroupRetriever
    {
        public AllTeacherGroupRetriever(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        protected override Expression<Func<DataGroup, bool>> SpecifyFilter(string uid, int pageIndex, int pageSize)
        {
            var groupIds = GetGroupIds(uid, pageIndex, pageSize);
            return x => x.OwnerId == uid || groupIds.Contains(x.Id);
        }

        private IEnumerable<Guid> GetGroupIds(string uid, int pageIndex, int pageSize)
        {
            var parameters = new QueryParameters<Teacher>
            {
                FilterRules = x => x.Uid == uid && (x.Status == UserStatus.Requested || x.Status == UserStatus.Accepted),
                PageRules = new PageSettings(pageIndex, pageSize)
            };
            return UnitOfWork.GetAll(parameters).Select(x => x.GroupId);
        }
    }
}
