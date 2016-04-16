using System;
using Teachersteams.Domain.Enums;

namespace Teachersteams.Domain.Entities
{
    public abstract class BaseUser: Entity
    {
        protected BaseUser() { }

        protected BaseUser(string uid, Guid groupId)
        {
            Uid = uid;
            GroupId = groupId;
            Status = UserStatus.Requested;
        }

        public string Uid { get; set; }

        public Guid GroupId { get; set; }

        public UserStatus Status { get; protected set; }
    }
}
