using System;

namespace Teachersteams.Business.ViewModels.Assignment
{
    public class AssignmentCheckViewModel
    {
        public Guid Id { get; set; }

        public Guid? AssigneeTeacherId { get; set; }

        public byte Grade { get; set; }

        public DateTime CheckDate { get; set; }
    }
}
