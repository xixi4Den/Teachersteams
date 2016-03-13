using AutoMapper;
using Teachersteams.Business.ViewModels;
using Teachersteams.Domain.Entities;

namespace Teachersteams.Business.Services.Mapping
{
    public class MapProfile: Profile
    {
        protected override void Configure()
        {
            MapGroupToGrouipTitleViewModel();
        }

        private void MapGroupToGrouipTitleViewModel()
        {
            CreateMap<Group, GroupTitleViewModel>()
                .ForMember(m => m.Id, s => s.MapFrom(o => o.Id))
                .ForMember(m => m.Title, s => s.MapFrom(o => o.Title))
                .ForMember(m => m.OwnerId, s => s.MapFrom(o => o.OwnerId));
        }
    }
}
