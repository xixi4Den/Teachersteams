using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Teachersteams.Business.Extensions;
using Teachersteams.Business.Retrievers.Group.Contract;
using Teachersteams.Business.ViewModels.Group;
using Teachersteams.Domain;
using Teachersteams.Domain.Query;
using DataGroup = Teachersteams.Domain.Entities.Group;

namespace Teachersteams.Business.Retrievers.Group
{
    public abstract class BaseGroupRetriever: IGroupRetriever
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        protected IUnitOfWork UnitOfWork
        {
            get { return unitOfWork; }
        }

        protected BaseGroupRetriever(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IEnumerable<GroupTitleViewModel> Retrieve(string uid, int pageIndex, int pageSize)
        {
            var parameters = new QueryParameters<DataGroup>
            {
                FilterRules = SpecifyFilter(uid, pageIndex, pageSize),
                PageRules = new PageSettings(pageIndex, pageSize),
                SortRules = x => x.OrderByDescending(group => group.CreateDate)
            };
            var groups = unitOfWork.GetAll(parameters);
            return mapper.MapManyTo<GroupTitleViewModel>(groups);
        }

        protected abstract Expression<Func<DataGroup, bool>> SpecifyFilter(string uid, int pageIndex, int pageSize);
    }
}
