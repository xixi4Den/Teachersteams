using System;

namespace Teachersteams.Domain.Entities
{
    public class Teacher: BaseUser
    {
        public Teacher()
        {
            
        }

        public Teacher(string uid, Guid groupId) : base(uid, groupId)
        {
        }
    }
}
