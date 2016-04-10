using System;
using System.ComponentModel.DataAnnotations;
using Teachersteams.Business.Enums;

namespace Teachersteams.Business.ViewModels.User
{
    public class UserBaseViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Uid { get; set; }

        [Required]
        public Guid GroupId { get; set; }

        public UserStatus Status { get; set; }
    }
}
