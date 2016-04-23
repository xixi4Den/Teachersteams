using AutoMapper;
using Teachersteams.Business.ViewModels;
using Teachersteams.Business.ViewModels.User;
using Teachersteams.Domain.Entities;

namespace Teachersteams.Business.Services.Mapping
{
    public class UserMapProfile: Profile
    {
        protected override void Configure()
        {
            MapUserToViewModel<Teacher, TeacherViewModel>();
            MapUserToViewModel<Student, StudentViewModel>();
            MapUserToRequestViewModel<Teacher>();
            MapUserToRequestViewModel<Student>();
        }

        private void MapUserToViewModel<TEntity, TViewModel>()
            where TEntity: BaseUser
            where TViewModel: UserBaseViewModel
        {
            CreateMap<TEntity, TViewModel>()
                .ForMember(m => m.Id, s => s.MapFrom(o => o.Id))
                .ForMember(m => m.Uid, s => s.MapFrom(o => o.Uid))
                .ForMember(m => m.GroupId, s => s.MapFrom(o => o.GroupId))
                .ForMember(m => m.Status, s => s.MapFrom(o => o.Status));
        }

        private void MapUserToRequestViewModel<TEntity>()
            where TEntity : BaseUser
        {
            CreateMap<TEntity, RequestViewModel>()
                .ForMember(m => m.UidTo, s => s.MapFrom(o => o.Uid))
                .ForMember(m => m.GroupId, s => s.MapFrom(o => o.GroupId))
                .ForMember(m => m.GroupName, s => s.MapFrom(o => o.Group.Title))
                .ForMember(m => m.Response, s => s.MapFrom(o => o.Status));
        }
    }
}
