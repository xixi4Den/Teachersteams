using System.Collections.Generic;
using Teachersteams.Business.Enums;
using Teachersteams.Business.ViewModels;
using Teachersteams.Shared.Dependency;

namespace Teachersteams.Business.Services
{
    public interface IGroupService : IRequestDependency
    {
        IEnumerable<GroupTitleViewModel> GetTeacherGroupTitles(string userId, GroupFilterType groupFilter, int pageIndex, int pageSize);
    }
}
