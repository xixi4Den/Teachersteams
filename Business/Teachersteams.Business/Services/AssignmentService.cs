using System;
using System.Collections.Generic;
using AutoMapper;
using Teachersteams.Business.Extensions;
using Teachersteams.Business.Helpers;
using Teachersteams.Business.ViewModels.Assignment;
using Teachersteams.Business.ViewModels.Grid;
using Teachersteams.Domain;
using Teachersteams.Domain.Entities;
using Teachersteams.Domain.Enums;
using Teachersteams.Domain.Query;
using Teachersteams.Shared.Validation;

namespace Teachersteams.Business.Services
{
    public class AssignmentService: IAssignmentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IGridOptionsHelper gridOptionsHelper;

        public AssignmentService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IGridOptionsHelper gridOptionsHelper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.gridOptionsHelper = gridOptionsHelper;
        }

        public AssignmentViewModel CreateAssignment(string uid, AssignmentViewModel viewModel)
        {
            Validate(uid, viewModel);

            var entity = mapper.Map<Assignment>(viewModel);
            entity.Status = AssignmentStatus.Active;
            var insertedEntity = unitOfWork.InsertOrUpdate(entity);
            unitOfWork.Commit();
            return mapper.Map<AssignmentViewModel>(insertedEntity);
        }

        public IEnumerable<AssignmentViewModel> GetAllAssignments(Guid groupId, GridOptions gridOptions)
        {
            var assignments = unitOfWork.GetAll(new QueryParameters<Assignment>
            {
                FilterRules = x => x.GroupId == groupId,
                PageRules = new PageSettings(gridOptions.PageNumber, gridOptions.PageSize),
                SortRules = gridOptionsHelper.BuidDynamicOrderedQuery<Assignment>(gridOptions)
            });
            return mapper.MapManyTo<AssignmentViewModel>(assignments);
        }

        public int AssignmentCount(Guid groupId)
        {
            return unitOfWork.Count(new QueryParameters<Assignment>
            {
                FilterRules = x => x.GroupId == groupId,
            });
        }

        private void Validate(string uid, AssignmentViewModel viewModel)
        {
            Contract.NotNull<ArgumentNullException>(viewModel);
            Contract.NotDefault<Guid, ArgumentException>(viewModel.GroupId);
            Contract.NotNullAndNotEmpty<ArgumentException>(viewModel.File);

            ValidateCreator(uid, viewModel);
        }

        private void ValidateCreator(string uid, AssignmentViewModel viewModel)
        {
            var isValidTeacher = unitOfWork.Any(new QueryParameters<Teacher>
            {
                FilterRules = x => x.Uid == uid && x.GroupId == viewModel.GroupId
            });

            var isOwner = unitOfWork.Any(new QueryParameters<Group>
            {
                FilterRules = x => x.Id == viewModel.GroupId && x.OwnerId == uid
            });

            Contract.Assert<UnauthorizedAccessException>(isValidTeacher || isOwner);
        }
    }
}
