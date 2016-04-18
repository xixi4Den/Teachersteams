using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Teachersteams.Business.Attributes;
using Teachersteams.Business.Enums;
using Teachersteams.Business.Extensions;
using Teachersteams.Business.Retrievers.Group.Contract;
using Teachersteams.Business.ViewModels.Group;
using Teachersteams.Domain;
using Teachersteams.Domain.Entities;
using Teachersteams.Domain.Query;
using DataGroup = Teachersteams.Domain.Entities.Group;

namespace Teachersteams.Business.Retrievers.Group
{
    [GroupRetrieverMeta(GroupFilterType.ForStudent)]
    public class StudentGroupRetriever: IGroupRetriever
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public StudentGroupRetriever(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IEnumerable<GroupTitleViewModel> Retrieve(string uid, int pageIndex, int pageSize)
        {
            var ids = GetGroupIds(uid, pageIndex, pageSize);
            var groups = GetGroups(ids, pageIndex, pageSize);
            return mapper.MapManyTo<GroupTitleViewModel>(groups);
        }

        private IEnumerable<DataGroup> GetGroups(IEnumerable<Guid> ids, int pageIndex, int pageSize)
        {
            var parameters = new QueryParameters<DataGroup>
            {
                FilterRules = x => ids.Contains(x.Id),
                PageRules = new PageSettings(pageIndex, pageSize)
            };
            return unitOfWork.GetAll(parameters);
        }

        private IEnumerable<Guid> GetGroupIds(string uid, int pageIndex, int pageSize)
        {
            var parameters = new QueryParameters<Student>
            {
                FilterRules = x => x.Uid == uid,
                PageRules = new PageSettings(pageIndex, pageSize)
            };
            return unitOfWork.GetAll(parameters).Select(x => x.GroupId);
        }
    }
}
