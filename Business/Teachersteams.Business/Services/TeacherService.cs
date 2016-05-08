using AutoMapper;
using Teachersteams.Business.Exceptions;
using Teachersteams.Business.Helpers;
using Teachersteams.Business.Services.Contracts;
using Teachersteams.Business.ViewModels.User;
using Teachersteams.Domain;
using Teachersteams.Domain.Entities;
using Teachersteams.Domain.Query;
using DataUserStatus = Teachersteams.Domain.Enums.UserStatus;

namespace Teachersteams.Business.Services
{
    public class TeacherService: UserBaseService<Teacher, TeacherViewModel>, ITeacherService
    {
        public TeacherService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IGridOptionsHelper gridOptionsHelper) : base(unitOfWork, mapper, gridOptionsHelper)
        {
            
        }

        protected override Teacher CreateNewUser(TeacherViewModel viewModel)
        {
            return new Teacher(viewModel.Uid, viewModel.GroupId);
        }

        protected override void ValidateUserIsNotTeacherAndStudentAtSameTime(TeacherViewModel viewModel)
        {
            var isTeacherExist = unitOfWork.Any(new QueryParameters<Student>
            {
                FilterRules = x => x.GroupId == viewModel.GroupId && x.Uid == viewModel.Uid && (x.Status == DataUserStatus.Accepted || x.Status == DataUserStatus.Requested)
            });

            if (isTeacherExist)
            {
                throw new UserCannotBeStudentAndTeacherException();
            }
        }
    }
}
