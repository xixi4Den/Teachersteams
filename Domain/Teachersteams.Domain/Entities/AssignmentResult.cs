using System;

namespace Teachersteams.Domain.Entities
{
    public class AssignmentResult: Entity
    {
        public Guid AssignmentId { get; set; }

        public Guid StudentId { get; set; }

        public DateTime CompletionDate { get; set; }

        public string File { get; set; }

        public Guid? AssigneeTeacherId { get; set; }

        public byte? Grade { get; set; }

        public DateTime? CheckDate { get; set; }



        public virtual Student Student { get; set; }

        public virtual Teacher AssigneeTeacher { get; set; }

        public virtual Assignment Assignment { get; set; }
    }
}
