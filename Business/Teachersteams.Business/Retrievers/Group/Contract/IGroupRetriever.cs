using System.Collections.Generic;
using Teachersteams.Business.ViewModels.Group;

namespace Teachersteams.Business.Retrievers.Group.Contract
{
    public interface IGroupRetriever
    {
        IEnumerable<GroupTitleViewModel> Retrieve(string ownerId, int pageIndex, int pageSize);
    }
}