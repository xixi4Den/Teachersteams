using AutoMapper;
using Teachersteams.Business.ViewModels.Assignment;
using Teachersteams.Domain.Entities;

namespace Teachersteams.Business.Services.Mapping
{
    public class AssignmentMapProfile: Profile
    {
        protected override void Configure()
        {
            MapAssignmentToAssignmentViewModel();
            MapAssignmentViewModelToAssignment();
            MapAssignmentCompletionViewModelToAssignmentResult();
        }

        private void MapAssignmentToAssignmentViewModel()
        {
            CreateMap<Assignment, AssignmentViewModel>()
                .ForMember(m => m.Id, s => s.MapFrom(o => o.Id))
                .ForMember(m => m.Title, s => s.MapFrom(o => o.Title))
                .ForMember(m => m.Description, s => s.MapFrom(o => o.Description))
                .ForMember(m => m.Status, s => s.MapFrom(o => o.Status))
                .ForMember(m => m.File, s => s.MapFrom(o => o.File))
                .ForMember(m => m.GroupId, s => s.MapFrom(o => o.GroupId))
                .ForMember(m => m.ExpirationDate, s => s.MapFrom(o => o.ExpirationDate))
                .ForMember(m => m.Creator, s => s.MapFrom(o => o.Creator));
        }

        private void MapAssignmentViewModelToAssignment()
        {
            CreateMap<AssignmentViewModel, Assignment>()
                .ForMember(m => m.Id, s => s.MapFrom(o => o.Id))
                .ForMember(m => m.Title, s => s.MapFrom(o => o.Title))
                .ForMember(m => m.Description, s => s.MapFrom(o => o.Description))
                .ForMember(m => m.Status, s => s.MapFrom(o => o.Status))
                .ForMember(m => m.File, s => s.MapFrom(o => o.File))
                .ForMember(m => m.GroupId, s => s.MapFrom(o => o.GroupId))
                .ForMember(m => m.ExpirationDate, s => s.MapFrom(o => o.ExpirationDate))
                .ForMember(m => m.Creator, s => s.MapFrom(o => o.Creator));
        }

        private void MapAssignmentCompletionViewModelToAssignmentResult()
        {
            CreateMap<AssignmentCompletionViewModel, AssignmentResult>()
                .ForMember(m => m.Id, s => s.MapFrom(o => o.Id))
                .ForMember(m => m.AssignmentId, s => s.MapFrom(o => o.AssignmentId))
                .ForMember(m => m.StudentId, s => s.MapFrom(o => o.StudentId))
                .ForMember(m => m.CompletionDate, s => s.MapFrom(o => o.CompletionDate))
                .ForMember(m => m.File, s => s.MapFrom(o => o.File));
        }
    }
}
