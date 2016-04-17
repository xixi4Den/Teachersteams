using System;
using System.Collections.Generic;
using Teachersteams.Business.Enums;
using Teachersteams.Business.Retrievers.Group.Contract;
using Teachersteams.Domain;
using Teachersteams.Business.Attributes;
using Teachersteams.Business.ViewModels.Group;

namespace Teachersteams.Business.Retrievers.Group
{

    [GroupRetrieverMeta(GroupFilterType.AllForTeacher)]
    public class AllTeacherGroupRetriever : IGroupRetriever
    {
        private readonly IUnitOfWork unitOfWork;

        public AllTeacherGroupRetriever(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<GroupTitleViewModel> Retrieve(string uid, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
