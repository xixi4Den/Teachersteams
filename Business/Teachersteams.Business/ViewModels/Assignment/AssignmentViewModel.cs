using System;
using System.ComponentModel.DataAnnotations;
using Teachersteams.Business.Enums;
using DataGroup = Teachersteams.Domain.Entities.Group;

namespace Teachersteams.Business.ViewModels.Assignment
{
    public class AssignmentViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public AssignmentStatus Status { get; set; }

        [Required]
        public string File { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        public Guid GroupId { get; set; }

        public Guid? Creator { get; set; }
    }
}
