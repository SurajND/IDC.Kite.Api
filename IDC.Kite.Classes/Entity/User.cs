using System;
using System.Collections.Generic;

namespace IDC.Kite.Classes.Entity
{
    public class User
    {
        public Guid Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public Guid? OperationalCompanyId { get; set; }
        public OperationalCompany OperationalCompany { get; set; }
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }
        public Guid? ProjectId { get; set; }
        public Project Project { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
