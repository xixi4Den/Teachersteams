using System;
using System.Collections.Generic;
using Autofac.Features.Indexed;
using AutoMapper;
using Teachersteams.Business.Enums;
using Teachersteams.Business.Retrievers.Group.Contract;
using Teachersteams.Business.ViewModels;
using Teachersteams.Domain;
using Teachersteams.Domain.Entities;

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

        public Guid CreateGroup(AddGroupViewModel viewModel)
        {
            var groupEntity = mapper.Map<Group>(viewModel);
            groupEntity.CreateDate = DateTime.UtcNow;
            unitOfWork.InsertOrUpdate(groupEntity);
            unitOfWork.Commit();
            return groupEntity.Id;
        }
    }
}