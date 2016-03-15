using System;

namespace Teachersteams.Business.ViewModels
{
    //[Serializable]
    public class AddGroupViewModel
    {
        public Guid Id
        {
            get;
            set;
        }

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
