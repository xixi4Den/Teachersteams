using System;

namespace Teachersteams.Domain.Entities
{
    public class Group: Entity
    {
        public string Title
        {
            get;
            set;
        }

        public string Description
        {
            get; 
            set;
        }

        public DateTime CreateDate
        {
            get; 
            set;
        }

        public string OwnerId
        {
            get;
            set;
        }

        public string PictureLink
        {
            get; 
            set;
        }
    }
}
