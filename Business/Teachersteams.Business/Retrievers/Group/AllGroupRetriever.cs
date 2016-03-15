using System;
using System.Collections.Generic;
using Teachersteams.Business.Enums;
using Teachersteams.Business.Retrievers.Group.Contract;
using Teachersteams.Business.ViewModels;
using Teachersteams.Domain;
using Teachersteams.Business.Attributes;

namespace Teachersteams.Business.Retrievers.Group
{

    [GroupRetrieverMeta(GroupFilterType.All)]
    public class AllGroupRetriever : IGroupRetriever
    {
        private readonly IUnitOfWork unitOfWork;

        public AllGroupRetriever(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<GroupTitleViewModel> Retrieve(string userId, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
