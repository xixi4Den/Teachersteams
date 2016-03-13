using System.Collections.Generic;
using Autofac.Features.Indexed;
using Teachersteams.Business.Enums;
using Teachersteams.Business.Retrievers.Group.Contract;
using Teachersteams.Business.ViewModels;

namespace Teachersteams.Business.Services
{
    public class GroupService: IGroupService
    {
        private readonly IIndex<GroupFilterType, IGroupRetriever> groupRetrievers;

        public GroupService(IIndex<GroupFilterType, IGroupRetriever> groupRetrievers)
        {
            this.groupRetrievers = groupRetrievers;
        }

        public IEnumerable<GroupTitleViewModel> GetTeacherGroupTitles(string userId, GroupFilterType filterType, int pageIndex, int pageSize)
        {
            return groupRetrievers[filterType].Retrieve(userId, pageIndex, pageSize);
        }
    }
}
