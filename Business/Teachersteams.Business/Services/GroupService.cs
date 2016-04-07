using System;
using System.Collections.Generic;
using Autofac.Features.Indexed;
using AutoMapper;
using Teachersteams.Business.Enums;
using Teachersteams.Business.Exceptions;
using Teachersteams.Business.Retrievers.Group.Contract;
using Teachersteams.Business.ViewModels;
using Teachersteams.Domain;
using Teachersteams.Domain.Entities;
using Teachersteams.Domain.Query;
using Teachersteams.Shared.Validation;

namespace Teachersteams.Business.Services
{
    public class GroupService: IGroupService
    {
        private readonly IIndex<GroupFilterType, IGroupRetriever> groupRetrievers;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GroupService(IIndex<GroupFilterType, IGroupRetriever> groupRetrievers,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.groupRetrievers = groupRetrievers;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IEnumerable<GroupTitleViewModel> GetTeacherGroupTitles(string userId, GroupFilterType filterType, int pageIndex, int pageSize)
        {
            return groupRetrievers[filterType].Retrieve(userId, pageIndex, pageSize);
        }

        public GroupTitleViewModel CreateGroup(AddGroupViewModel viewModel)
        {
            Contract.NotNull<ArgumentNullException>(viewModel);
            CheckViewModelTitle(viewModel);
            var groupEntity = mapper.Map<Group>(viewModel);
            groupEntity.CreateDate = DateTime.UtcNow;
            unitOfWork.InsertOrUpdate(groupEntity);
            unitOfWork.Commit();
            return mapper.Map<GroupTitleViewModel>(groupEntity);
        }

        private void CheckViewModelTitle(AddGroupViewModel viewModel)
        {
            Contract.NotNullAndNotEmpty<ArgumentNullException>(viewModel.Title);

            var isTitleAvailable = !unitOfWork.Any(new QueryParameters<Group>
            {
                FilterRules = x => x.Title == viewModel.Title
            });

            if (!isTitleAvailable)
            {
                throw new GroupTitleAlreadyExistsException(viewModel.Title);
            }
        }
    }
}