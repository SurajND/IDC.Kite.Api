using IDC.Kite.Classes.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IDC.Kite.Business.Dto.v1.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        [MaxLength(20)]
        [Required(ErrorMessage = "FirstName is required")]
        public string FName { get; set; }
        [MaxLength(20)]
        public string LName { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public Guid? OperationalCompanyId { get; set; }
        public string Token { get; set; }
        public Guid? ProjectId { get; set; }
        [Required]
        public Guid RoleId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
