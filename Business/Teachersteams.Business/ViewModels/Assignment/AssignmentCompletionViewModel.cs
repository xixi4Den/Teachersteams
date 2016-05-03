using System;

namespace Teachersteams.Business.ViewModels.Assignment
{
    public class AssignmentCompletionViewModel
    {
        public Guid Id { get; set; }

        public Guid AssignmentId { get; set; }

        public Guid StudentId { get; set; }

        public DateTime CompletionDate { get; set; }

        public string File { get; set; }
    }
}
