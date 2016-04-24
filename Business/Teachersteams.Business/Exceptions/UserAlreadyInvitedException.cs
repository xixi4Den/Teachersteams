using System;

namespace Teachersteams.Business.Exceptions
{
    public class UserAlreadyInvitedException: BusinessException
    {
        private const string messagePattern = "This user has already been invited to participate in this group.";

        public UserAlreadyInvitedException()
            : base(String.Format(messagePattern))
        { 
        }
    }
}