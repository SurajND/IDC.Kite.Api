using System;
using System.Collections.Generic;

namespace IDC.Kite.Classes.Entity
{
    public class Permission
    {
        public Permission()
        {
            RolePermissions = new List<RolePermission>();
        }
        public Guid Id { get; set; }
        public string PermissionType { get; set; }
        public IList<RolePermission> RolePermissions { get; set; }
    }
}
