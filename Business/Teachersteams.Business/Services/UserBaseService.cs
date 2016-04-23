﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Teachersteams.Business.Enums;
using Teachersteams.Business.Extensions;
using Teachersteams.Business.Helpers;
using Teachersteams.Business.ViewModels;
using Teachersteams.Business.ViewModels.Grid;
using Teachersteams.Business.ViewModels.User;
using Teachersteams.Domain;
using Teachersteams.Domain.Entities;
using Teachersteams.Domain.Exceptions;
using Teachersteams.Domain.Query;
using Teachersteams.Shared.Validation;
using DataUserStatus = Teachersteams.Domain.Enums.UserStatus;

namespace Teachersteams.Business.Services
{
    public abstract class UserBaseService<TEntity, TViewModel>
        where TEntity : BaseUser
        where TViewModel : UserBaseViewModel
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IGridOptionsHelper gridOptionsHelper;

        private readonly IDictionary<UserType, IEnumerable<DataUserStatus>> applicableUserStatuses = new Dictionary
            <UserType, IEnumerable<DataUserStatus>>
        {
            {UserType.Student, new List<DataUserStatus> {DataUserStatus.Accepted}},
            {UserType.Teacher, new List<DataUserStatus> {DataUserStatus.Accepted, DataUserStatus.Requested}}
        };

        protected UserBaseService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IGridOptionsHelper gridOptionsHelper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.gridOptionsHelper = gridOptionsHelper;
        }

        public virtual TViewModel Invite(TViewModel viewModel)
        {
            Contract.NotNull<ArgumentNullException>(viewModel);
            Contract.NotNullAndNotEmpty<ArgumentException>(viewModel.Uid);
            Contract.NotDefault<Guid, ArgumentException>(viewModel.GroupId);

            var newEntity = CreateNewUser(viewModel);
            var insertedEntity = unitOfWork.InsertOrUpdate(newEntity);
            unitOfWork.Commit();
            return mapper.Map<TViewModel>(insertedEntity);
        }

        protected abstract TEntity CreateNewUser(TViewModel viewModel);

        public virtual IEnumerable<TViewModel> GetUsers(Guid groupId, GridOptions gridOptions, UserType userType)
        {
            Contract.NotDefault<Guid, ArgumentException>(groupId);

            var statuses = applicableUserStatuses[userType];

            var teachers = unitOfWork.GetAll(new QueryParameters<TEntity>
            {
                FilterRules = x => x.GroupId == groupId && statuses.Contains(x.Status),
                PageRules = new PageSettings(gridOptions.PageNumber, gridOptions.PageSize),
                SortRules = gridOptionsHelper.BuidDynamicOrderedQuery<TEntity>(gridOptions)
            });

            return mapper.MapManyTo<TViewModel>(teachers);
        }

        public virtual int Count(Guid groupId)
        {
            Contract.NotDefault<Guid, ArgumentException>(groupId);

            return unitOfWork.Count(new QueryParameters<TEntity>
            {
                FilterRules = x => x.GroupId == groupId
            });
        }

        public virtual IEnumerable<RequestViewModel> GetRequests(string uid)
        {
            Contract.NotNullAndNotEmpty<ArgumentException>(uid);

            var requests = unitOfWork.GetAll(new QueryParameters<TEntity>
            {
                FilterRules = x => x.Uid == uid && x.Status == Domain.Enums.UserStatus.Requested,
                PageRules = new PageSettings(1, 20),
            });

            return mapper.MapManyTo<RequestViewModel>(requests);
        }

        public virtual bool AnyRequest(string uid, Guid groupId)
        {
            Contract.NotNullAndNotEmpty<ArgumentException>(uid);
            Contract.NotDefault<Guid, ArgumentException>(groupId);

            return unitOfWork.Any(new QueryParameters<TEntity>
            {
                FilterRules = x => x.Uid == uid && x.Status == Domain.Enums.UserStatus.Requested && x.GroupId == groupId,
                PageRules = new PageSettings(1, 20),
            });
        }

        public virtual int RequestsCount(string uid)
        {
            Contract.NotNullAndNotEmpty<ArgumentException>(uid);

            return unitOfWork.Count(new QueryParameters<TEntity>
            {
                FilterRules = x => x.Uid == uid && x.Status == Domain.Enums.UserStatus.Requested
            });
        }

        public virtual void Response(RequestViewModel viewModel)
        {
            Contract.NotNull<ArgumentNullException>(viewModel);
            Contract.NotNullAndNotEmpty<ArgumentException>(viewModel.UidTo);
            Contract.NotDefault<Guid, ArgumentException>(viewModel.GroupId);

            var user = unitOfWork.GetFirstOrDefault(new QueryParameters<TEntity>
            {
                FilterRules = x => x.Uid == viewModel.UidTo && x.GroupId == viewModel.GroupId,
            });

            Contract.NotNull<DataNotFoundException>(user);
            user.ResponseToInvitation((DataUserStatus) viewModel.Response);
            unitOfWork.InsertOrUpdate(user);
            unitOfWork.Commit();
        }
    }
}
