using System;
using System.Collections.Generic;

namespace IDC.Kite.Classes.Entity
{
    public class Role
    {
        public Role()
        {
            RolePermissions = new List<RolePermission>();
        }
        public Guid Id { get; set; }
        public string RoleType { get; set; }
        public IList<RolePermission> RolePermissions { get; set; }
    }
}
