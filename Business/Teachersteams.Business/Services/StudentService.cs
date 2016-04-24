using AutoMapper;
using Teachersteams.Business.Exceptions;
using Teachersteams.Business.Helpers;
using Teachersteams.Business.ViewModels.User;
using Teachersteams.Domain;
using Teachersteams.Domain.Entities;
using Teachersteams.Domain.Query;
using DataUserStatus = Teachersteams.Domain.Enums.UserStatus;

namespace Teachersteams.Business.Services
{
    public class StudentService: UserBaseService<Student, StudentViewModel>, IStudentService
    {
        public StudentService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IGridOptionsHelper gridOptionsHelper) : base(unitOfWork, mapper, gridOptionsHelper)
        {
            
        }

        protected override Student CreateNewUser(StudentViewModel viewModel)
        {
            return new Student(viewModel.Uid, viewModel.GroupId);
        }

        protected override void ValidateUserIsNotTeacherAndStudentAtSameTime(StudentViewModel viewModel)
        {
            var isTeacherExist = unitOfWork.Any(new QueryParameters<Teacher>
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
