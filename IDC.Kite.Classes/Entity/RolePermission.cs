using System;

namespace IDC.Kite.Classes.Entity
{
    public class RolePermission
    {
        public Role Role { get; set; }
        public Guid RoleId { get; set; }
        public Permission Permission { get; set; }
        public Guid PermissionId { get; set; }
    }
}
