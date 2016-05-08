using System.Linq;
using AutoMapper;
using Teachersteams.Business.ViewModels.Assignment;
using Teachersteams.Business.ViewModels.Board;
using Teachersteams.Domain.Entities;

namespace Teachersteams.Business.Services.Mapping
{
    public class AssignmentMapProfile: Profile
    {
        protected override void Configure()
        {
            MapAssignmentToAssignmentViewModel<AssignmentViewModel>();
            MapAssignmentToAssignmentViewModel<AssignmentViewModelForStudent>();
            MapAssignmentViewModelToAssignment();
            MapAssignmentCompletionViewModelToAssignmentResult();
            MapAssignmentResultToAssignmentResultViewModel();
            MapAssignmentToStudentBoardItemViewModel();
        }

        private void MapAssignmentToAssignmentViewModel<T>() where T: AssignmentViewModel
        {
            CreateMap<Assignment, T>()
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
                .ForMember(m => m.AssignmentId, s => s.MapFrom(o => o.AssignmentId))
                .ForMember(m => m.File, s => s.MapFrom(o => o.File));
        }

        private void MapAssignmentResultToAssignmentResultViewModel()
        {
            CreateMap<AssignmentResult, AssignmentResultViewModel>()
                .ForMember(m => m.Id, s => s.MapFrom(o => o.Id))
                .ForMember(m => m.AssignmentId, s => s.MapFrom(o => o.AssignmentId))
                .ForMember(m => m.StudentUid, s => s.MapFrom(o => o.Student.Uid))
                .ForMember(m => m.CompletionDate, s => s.MapFrom(o => o.CompletionDate))
                .ForMember(m => m.File, s => s.MapFrom(o => o.File))
                .ForMember(m => m.AssigneeTeacherUid, s => s.MapFrom(o => o.AssigneeTeacher.Uid))
                .ForMember(m => m.Grade, s => s.MapFrom(o => o.Grade))
                .ForMember(m => m.CheckDate, s => s.MapFrom(o => o.CheckDate));
        }

        private void MapAssignmentToStudentBoardItemViewModel()
        {
            CreateMap<Assignment, StudentBoardItemViewModel>()
                .ForMember(m => m.AssignmentId, s => s.MapFrom(o => o.Id))
                .ForMember(m => m.Title, s => s.MapFrom(o => o.Title))
                .ForMember(m => m.AssignmentFile, s => s.MapFrom(o => o.File))
                .ForMember(m => m.ExpirationDate, s => s.MapFrom(o => o.ExpirationDate))
                .ForMember(m => m.GroupId, s => s.MapFrom(o => o.GroupId))
                .ForMember(m => m.GroupTitle, s => s.MapFrom(o => o.Group.Title));
        }
    }
}
