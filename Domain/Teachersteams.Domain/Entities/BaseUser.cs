using System;
using Teachersteams.Domain.Enums;
using Teachersteams.Shared.Validation;

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

        public virtual Group Group { get; set; }

        public UserStatus Status { get; protected set; }

        public void ResponseToInvitation(UserStatus response)
        {
            Contract.Assert<InvalidOperationException>(Status == UserStatus.Requested);
            Contract.Assert<InvalidOperationException>(response == UserStatus.Accepted || response == UserStatus.Declined);

            Status = response;
        }

        public void Delete()
        {
            Contract.Assert<InvalidOperationException>(Status == UserStatus.Requested || Status == UserStatus.Accepted);

            Status = UserStatus.Deleted;
        }
    }
}
