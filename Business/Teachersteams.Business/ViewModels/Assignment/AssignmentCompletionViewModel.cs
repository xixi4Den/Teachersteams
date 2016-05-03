using System;
using System.ComponentModel.DataAnnotations;

namespace Teachersteams.Business.ViewModels.Assignment
{
    public class AssignmentCompletionViewModel
    {
        [Required]
        public Guid AssignmentId { get; set; }

        [Required]
        public string File { get; set; }
    }
}
