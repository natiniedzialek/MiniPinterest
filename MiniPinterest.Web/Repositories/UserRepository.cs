using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniPinterest.Web.Controllers;
using MiniPinterest.Web.Data;

namespace MiniPinterest.Web.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext authDbContext;

        public UserRepository(AuthDbContext authDbContext)
        {
            this.authDbContext = authDbContext;
        }

        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            var users = await authDbContext.Users.ToListAsync();

            var admin = await authDbContext.Users
                .FirstOrDefaultAsync(x => x.Email == "admin@minipinterest.com");
            
            users.Remove(admin);
            return users;
        }
    }
}
