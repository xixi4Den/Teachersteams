using AutoMapper;
using Teachersteams.Business.Helpers;
using Teachersteams.Business.ViewModels.User;
using Teachersteams.Domain;
using Teachersteams.Domain.Entities;

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
    }
}
