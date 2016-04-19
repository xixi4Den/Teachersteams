using System;
using System.Linq.Expressions;
using AutoMapper;
using Teachersteams.Business.Attributes;
using Teachersteams.Business.Enums;
using Teachersteams.Domain;
using DataGroup = Teachersteams.Domain.Entities.Group;

namespace Teachersteams.Business.Retrievers.Group
{
    [GroupRetrieverMeta(GroupFilterType.Own)]
    public class OwnGroupRetriever: BaseGroupRetriever
    {
        public OwnGroupRetriever(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override Expression<Func<DataGroup, bool>> SpecifyFilter(string uid, int pageIndex, int pageSize)
        {
            return x => x.OwnerId == uid;
        }
    }
}
