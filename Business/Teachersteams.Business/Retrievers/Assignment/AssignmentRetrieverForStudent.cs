using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.Internal;
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
    [UserTypeSpecificRetrieverMeta(UserType.Student)]
    public class AssignmentRetrieverForStudent: BaseAssignmentRetriever
    {
        private readonly IMapper mapper;

        public AssignmentRetrieverForStudent(IUnitOfWork unitOfWork,
            IGridOptionsHelper gridOptionsHelper,
            IMapper mapper) : base(unitOfWork, gridOptionsHelper)
        {
            this.mapper = mapper;
        }

        public override IEnumerable<AssignmentViewModel> Retrieve(Guid groupId, string uid, GridOptions gridOptions)
        {
            var assignments = RetrieveInternal(groupId, gridOptions).ToList();
            var viewModels = mapper.MapManyTo<AssignmentViewModelForStudent>(assignments).ToList();
            viewModels.Each(viewModel =>
            {
                var entity = assignments.Single(x => x.Id == viewModel.Id);
                viewModel.IsCompleted = entity.Results.Any(x => x.Student.Uid == uid);
            });
            return viewModels;
        }
    }
}
