using IDC.Kite.Classes.Entity;
using Microsoft.EntityFrameworkCore;

namespace IDC.Kite.DataModel
{
    public class KiteContext : DbContext
    {
        public KiteContext(DbContextOptions<KiteContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<OperationalCompany> OperationalCompanies { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<KeyIndicator> KeyIndicators { get; set; }
        public DbSet<ProjectKeyIndicator> ProjectKeyIndicators { get; set; }
        public DbSet<ProjectKeyIndicatorYear> ProjectKeyIndicatorYears { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolePermission>().HasKey(sc => new { sc.PermissionId , sc.RoleId });

            modelBuilder.Entity<RolePermission>()
                .HasOne<Role>(sc => sc.Role)
                .WithMany(s => s.RolePermissions)
                .HasForeignKey(sc => sc.RoleId);


            modelBuilder.Entity<RolePermission>()
                .HasOne<Permission>(sc => sc.Permission)
                .WithMany(s => s.RolePermissions)
                .HasForeignKey(sc => sc.PermissionId);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
