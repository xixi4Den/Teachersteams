using System;
using Teachersteams.Business.Enums;

namespace Teachersteams.Business.ViewModels
{
    public class RequestViewModel
    {
        public string UidTo { get; set; }

        public Guid GroupId { get; set; }

        public string GroupName { get; set; }

        public UserStatus Response {get; set; }
    }
}
