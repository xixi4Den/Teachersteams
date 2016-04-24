using System;

namespace Teachersteams.Business.Exceptions
{
    public class UserCannotBeStudentAndTeacherException: BusinessException
    {
        private const string messagePattern = "A user cannot be a teacher and a student at the same time.";

        public UserCannotBeStudentAndTeacherException()
            : base(String.Format(messagePattern))
        { 
        }
    }
}