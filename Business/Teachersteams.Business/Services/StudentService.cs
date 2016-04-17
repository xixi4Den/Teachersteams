using AutoMapper;
using Teachersteams.Business.Helpers;
using Teachersteams.Business.ViewModels.User;
using Teachersteams.Domain;
using Teachersteams.Domain.Entities;

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
    }
}
