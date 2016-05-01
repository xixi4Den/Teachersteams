using System;
using AutoMapper;
using Teachersteams.Business.ViewModels.Assignment;
using Teachersteams.Domain;
using Teachersteams.Domain.Entities;
using Teachersteams.Domain.Enums;
using Teachersteams.Domain.Query;
using Teachersteams.Shared.Validation;

namespace Teachersteams.Business.Services
{
    public class AssignmentService: IAssignmentService
    {
        public readonly IUnitOfWork unitOfWork;
        public readonly IMapper mapper;

        public AssignmentService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public AssignmentViewModel Create(string uid, AssignmentViewModel viewModel)
        {
            Validate(uid, viewModel);

            var entity = mapper.Map<Assignment>(viewModel);
            entity.Status = AssignmentStatus.Active;
            var insertedEntity = unitOfWork.InsertOrUpdate(entity);
            unitOfWork.Commit();
            return mapper.Map<AssignmentViewModel>(insertedEntity);
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
