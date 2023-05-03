using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MiniPinterest.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // add roles
            var adminRoleId = "4dec0650-556e-4eec-90d6-c25ac346fd16";
            var userRoleId = "06c7e0c3-cecf-4429-afc9-593a27a6ff7f";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                },
            };

            builder.Entity<IdentityRole>().HasData(roles);

            // create admin
            var adminId = "8a29ae5c-49a6-4cb7-a1b6-f4d15d3bd87e";
            var adminUser = new IdentityUser
            {
                UserName = "admin@minipinterest.com",
                Email = "admin@minipinterest.com",
                NormalizedEmail = "admin@minipinterest.com".ToUpper(),
                NormalizedUserName = "admin@minipinterest.com".ToUpper(),
                Id = adminId
            };

            adminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(adminUser, "password123");

            builder.Entity<IdentityUser>().HasData(adminUser);

            // add roles to users
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = adminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = adminId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}
