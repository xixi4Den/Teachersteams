using System;

namespace Teachersteams.Business.ViewModels.Board
{
    public class StudentBoardItemViewModel
    {
        public Guid GroupId { get; set; }

        public string GroupTitle { get; set; }

        public Guid AssignmentId { get; set; }

        public string Title { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string AssignmentFile { get; set; }

        public Guid? AssignmentResultId { get; set; }

        public DateTime? CompletionDate { get; set; }

        public string AssignmentResultFile { get; set; }

        public string AssigneeTeacherUid { get; set; }

        public byte? Grade { get; set; }

        public DateTime? CheckDate { get; set; }
    }
}
