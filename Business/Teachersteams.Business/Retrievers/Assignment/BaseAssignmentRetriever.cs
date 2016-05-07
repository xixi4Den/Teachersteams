using System;
using System.Collections.Generic;
using Teachersteams.Business.Helpers;
using Teachersteams.Business.ViewModels.Assignment;
using Teachersteams.Business.ViewModels.Grid;
using Teachersteams.Domain;
using Teachersteams.Domain.Query;
using DataAssignment = Teachersteams.Domain.Entities.Assignment;

namespace Teachersteams.Business.Retrievers.Assignment
{
    public abstract class BaseAssignmentRetriever : IAssignmentRetriever
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IGridOptionsHelper gridOptionsHelper;

        protected BaseAssignmentRetriever(IUnitOfWork unitOfWork,
            IGridOptionsHelper gridOptionsHelper)
        {
            this.unitOfWork = unitOfWork;
            this.gridOptionsHelper = gridOptionsHelper;
        }

        public abstract IEnumerable<AssignmentViewModel> Retrieve(Guid groupId, string uid, GridOptions gridOptions);

        protected IEnumerable<DataAssignment> RetrieveInternal(Guid groupId, GridOptions gridOptions)
        {
            return unitOfWork.GetAll(new QueryParameters<DataAssignment>
            {
                FilterRules = x => x.GroupId == groupId,
                PageRules = new PageSettings(gridOptions.PageNumber, gridOptions.PageSize),
                SortRules = gridOptionsHelper.BuidDynamicOrderedQuery<DataAssignment>(gridOptions)
            });
        }
    }
}
