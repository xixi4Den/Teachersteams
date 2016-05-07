using System;
using System.Collections.Generic;
using Teachersteams.Business.ViewModels.Assignment;
using Teachersteams.Business.ViewModels.Grid;

namespace Teachersteams.Business.Retrievers.Assignment
{
    public interface IAssignmentRetriever
    {
        IEnumerable<AssignmentViewModel> Retrieve(Guid groupId, string uid, GridOptions gridOptions);
    }
}
