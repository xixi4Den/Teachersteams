using AutoMapper;
using Teachersteams.Business.ViewModels;
using Teachersteams.Domain.Entities;

namespace Teachersteams.Business.Services.Mapping
{
    public class GroupMapProfile: Profile
    {
        protected override void Configure()
        {
            MapGroupToGroupTitleViewModel();
            MapAddGroupViewModelToGroup();
            MapGroupToGroupInfoViewModel();
        }

        private void MapGroupToGroupTitleViewModel()
        {
            CreateMap<Group, GroupTitleViewModel>()
                .ForMember(m => m.Id, s => s.MapFrom(o => o.Id))
                .ForMember(m => m.Title, s => s.MapFrom(o => o.Title))
                .ForMember(m => m.OwnerId, s => s.MapFrom(o => o.OwnerId));
        }

        private void MapAddGroupViewModelToGroup()
        {
            CreateMap<AddGroupViewModel, Group>()
                .ForMember(m => m.Title, s => s.MapFrom(o => o.Title))
                .ForMember(m => m.Description, s => s.MapFrom(o => o.Description))
                .ForMember(m => m.OwnerId, s => s.MapFrom(o => o.OwnerId))
                .ForMember(m => m.PictureLink, s => s.MapFrom(o => o.PictureLink));
        }

        private void MapGroupToGroupInfoViewModel()
        {
            CreateMap<Group, GroupInfoViewModel>()
                .ForMember(m => m.Title, s => s.MapFrom(o => o.Title))
                .ForMember(m => m.Description, s => s.MapFrom(o => o.Description))
                .ForMember(m => m.OwnerId, s => s.MapFrom(o => o.OwnerId))
                .ForMember(m => m.PictureLink, s => s.MapFrom(o => o.PictureLink));
        }
    }
}
