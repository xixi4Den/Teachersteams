using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Teachersteams.Business.Enums;
using Teachersteams.Business.Helpers;
using Teachersteams.Business.ViewModels.Grid;
using Teachersteams.Business.ViewModels.User;
using Teachersteams.Domain;
using Teachersteams.Domain.Entities;
using Teachersteams.Domain.Query;
using Teachersteams.Shared.Validation;

namespace Teachersteams.Business.Services
{
    public class UserService: IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IGridOptionsHelper gridOptionsHelper;

        public UserService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IGridOptionsHelper gridOptionsHelper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.gridOptionsHelper = gridOptionsHelper;
        }

        public TeacherViewModel InviteTeacher(TeacherViewModel viewModel)
        {
            Contract.NotNull<ArgumentNullException>(viewModel);
            Contract.NotNullAndNotEmpty<ArgumentException>(viewModel.Uid);
            Contract.NotDefault<Guid, ArgumentException>(viewModel.GroupId);

            var newEntity = new Teacher(viewModel.Uid, viewModel.GroupId);
            var insertedEntity = unitOfWork.InsertOrUpdate(newEntity);
            unitOfWork.Commit();
            return mapper.Map<TeacherViewModel>(insertedEntity);
        }

        public IEnumerable<TeacherViewModel> GetTeachers(Guid groupId, GridOptions gridOptions)
        {
            Contract.NotDefault<Guid, ArgumentException>(groupId);

            var teachers = unitOfWork.GetAll(new QueryParameters<Teacher>
            {
                FilterRules = x => x.GroupId == groupId,
                PageRules = new PageSettings(gridOptions.PageNumber, gridOptions.PageSize),
                SortRules = gridOptionsHelper.BuidDynamicOrderedQuery<Teacher>(gridOptions)
            });

            return mapper.Map<IEnumerable<TeacherViewModel>>(teachers);
        }

        public int Count(Guid groupId)
        {
            Contract.NotDefault<Guid, ArgumentException>(groupId);

            return unitOfWork.Count(new QueryParameters<Teacher>
            {
                FilterRules = x => x.GroupId == groupId
            });
        }
    }
}
