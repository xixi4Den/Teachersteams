using System;
using System.Collections.Generic;
using Teachersteams.Business.Enums;
using Teachersteams.Business.ViewModels.Group;
using Teachersteams.Shared.Dependency;

namespace Teachersteams.Business.Services
{
    public interface IGroupService : IRequestDependency
    {
        IEnumerable<GroupTitleViewModel> GetGroupTitles(string uid, GroupFilterType groupFilter, int pageIndex, int pageSize);

        GroupTitleViewModel CreateGroup(AddGroupViewModel viewModel);

        GroupInfoViewModel GetGroupInfo(Guid groupId);
    }
}
