using System;
using System.Collections.Generic;
using AutoMapper;
using Teachersteams.Business.Attributes;
using Teachersteams.Business.Enums;
using Teachersteams.Business.Extensions;
using Teachersteams.Business.Helpers;
using Teachersteams.Business.ViewModels.Assignment;
using Teachersteams.Business.ViewModels.Grid;
using Teachersteams.Domain;
using DataAssignment = Teachersteams.Domain.Entities.Assignment;

namespace Teachersteams.Business.Retrievers.Assignment
{
    [UserTypeSpecificRetrieverMeta(UserType.Teacher)]
    public class AssignmentRetrieverForTeacher: BaseAssignmentRetriever
    {
        private readonly IMapper mapper;

        public AssignmentRetrieverForTeacher(IUnitOfWork unitOfWork,
            IGridOptionsHelper gridOptionsHelper,
            IMapper mapper) : base(unitOfWork, gridOptionsHelper)
        {
            this.mapper = mapper;
        }

        public override IEnumerable<AssignmentViewModel> Retrieve(Guid groupId, string uid, GridOptions gridOptions)
        {
            var assignments = RetrieveInternal(groupId, gridOptions);
            return mapper.MapManyTo<AssignmentViewModel>(assignments);
        }
    }
}
