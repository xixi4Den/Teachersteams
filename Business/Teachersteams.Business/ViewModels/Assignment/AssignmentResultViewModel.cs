using System;
using System.ComponentModel.DataAnnotations;

namespace Teachersteams.Business.ViewModels.Assignment
{
    public class AssignmentResultViewModel
    {
        public Guid Id { get; set; }

        public Guid AssignmentId { get; set; }

        public string StudentUid { get; set; }

        public DateTime CompletionDate { get; set; }

        [Required]
        public string File { get; set; }

        public string AssigneeTeacherUid { get; set; }

        public byte? Grade { get; set; }

        public DateTime? CheckDate { get; set; }
    }
}
