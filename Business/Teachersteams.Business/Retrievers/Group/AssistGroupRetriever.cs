using System;
using System.Collections.Generic;
using Teachersteams.Business.Attributes;
using Teachersteams.Business.Enums;
using Teachersteams.Business.Retrievers.Group.Contract;
using Teachersteams.Business.ViewModels;
using Teachersteams.Domain;

namespace Teachersteams.Business.Retrievers.Group
{

    [GroupRetrieverMeta(GroupFilterType.Assist)]
    public class AssistGroupRetriever : IGroupRetriever
    {
        private readonly IUnitOfWork unitOfWork;

        public AssistGroupRetriever(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<GroupTitleViewModel> Retrieve(string userId, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
