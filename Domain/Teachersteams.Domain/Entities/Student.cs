﻿using System;

namespace Teachersteams.Domain.Entities
{
    public class Student: BaseUser
    {
        public Student()
        {
            
        }

        public Student(string uid, Guid groupId) : base(uid, groupId)
        {
        }
    }
}
