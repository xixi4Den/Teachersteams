using System;
using System.Collections.Generic;
using Teachersteams.Business.Attributes;
using Teachersteams.Business.Enums;
using Teachersteams.Business.Retrievers.Group.Contract;
using Teachersteams.Business.ViewModels.Group;
using Teachersteams.Domain;

namespace Teachersteams.Business.Retrievers.Group
{

    [GroupRetrieverMeta(GroupFilterType.ForTeacherAssistant)]
    public class TeacherAssistantRetriever : IGroupRetriever
    {
        private readonly IUnitOfWork unitOfWork;

        public TeacherAssistantRetriever(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<GroupTitleViewModel> Retrieve(string uid, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
