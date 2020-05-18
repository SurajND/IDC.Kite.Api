using IDC.Kite.Classes.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDC.Kite.DataModel.Seed
{
    public static class DbSeeder
    {
        public static void SeedDb(this KiteContext context)
        {
            SeedRoles(context);
            SeedPermissions(context);          
        }

        private static void SeedRoles(KiteContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }

            var roles = new List<Role>()
            {
                new Role()
                {
                    RoleType = "Admin"
                },
                new Role()
                {
                    RoleType = "IDC Lead"
                },
                new Role()
                {
                    RoleType = "Opco Lead"
                },
                new Role()
                {
                    RoleType = "Project Lead"
                }
            };
            context.Roles.AddRange(roles);
            context.SaveChangesAsync();
        }

        private static void SeedPermissions(KiteContext context)
        {
            if (context.Permissions.Any())
            {
                return;
            }

            var permisssions = new List<Permission>()
            {
                new Permission()
                {
                    PermissionType = "Create"
                },
                new Permission()
                {
                    PermissionType = "Read"
                },
                new Permission()
                {
                    PermissionType = "Update"
                },
                new Permission()
                {
                    PermissionType = "Delete"
                }
            };
            context.Permissions.AddRange(permisssions);
            context.SaveChangesAsync();
        }
    }
}
