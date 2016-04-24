using System;

namespace Teachersteams.Business.Exceptions
{
    public class InvitedUserIsGroupOwnerException: BusinessException
    {
        private const string messagePattern = "The group owner cannot be invited to participate in the group neither as teacher nor student.";

        public InvitedUserIsGroupOwnerException()
            : base(String.Format(messagePattern))
        { 
        }
    }
}