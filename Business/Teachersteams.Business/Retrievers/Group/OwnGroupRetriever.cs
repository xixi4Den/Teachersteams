using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Teachersteams.Business.Attributes;
using Teachersteams.Business.Enums;
using Teachersteams.Business.Extensions;
using Teachersteams.Business.Retrievers.Group.Contract;
using Teachersteams.Business.ViewModels.Group;
using Teachersteams.Domain;
using Teachersteams.Domain.Query;

namespace Teachersteams.Business.Retrievers.Group
{
    [GroupRetrieverMeta(GroupFilterType.Own)]
    public class OwnGroupRetriever: IGroupRetriever
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public OwnGroupRetriever(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IEnumerable<GroupTitleViewModel> Retrieve(string userId, int pageIndex, int pageSize)
        {
            var parameters = new QueryParameters<Domain.Entities.Group>
            {
                FilterRules = x => x.OwnerId == userId,
                PageRules = new PageSettings(pageIndex, pageSize),
                SortRules = x => x.OrderByDescending(group => group.CreateDate)
            };
            var groups = unitOfWork.GetAll(parameters);
            return mapper.MapManyTo<GroupTitleViewModel>(groups);
        }
    }
}
