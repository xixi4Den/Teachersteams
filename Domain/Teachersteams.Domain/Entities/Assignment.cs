using System;
using System.Collections;
using System.Collections.Generic;
using Teachersteams.Domain.Enums;

namespace Teachersteams.Domain.Entities
{
    public class Assignment: Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public AssignmentStatus Status { get; set; }

        public string File { get; set; }

        public DateTime ExpirationDate { get; set; }

        public Guid GroupId { get; set; }

        public virtual Group Group { get; set; }

        public Guid? Creator { get; set; }

        public virtual ICollection<AssignmentResult> Results { get; set; }
    }
}
