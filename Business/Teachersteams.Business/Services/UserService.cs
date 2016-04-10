using System;
using AutoMapper;
using Teachersteams.Business.ViewModels.User;
using Teachersteams.Domain;
using Teachersteams.Domain.Entities;
using Teachersteams.Shared.Validation;

namespace Teachersteams.Business.Services
{
    public class UserService: IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UserService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
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
    }
}
