using System;

namespace Teachersteams.Business.Exceptions
{
    public class GroupTitleAlreadyExistsException: BusinessException
    {
        private const string messagePattern = "Cannot create a group because the group with title \"{0}\" already exists.";

        public GroupTitleAlreadyExistsException(string title)
            : base(String.Format(messagePattern, title))
        { 
        }
    }
}
