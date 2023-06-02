using Microsoft.AspNetCore.Identity;

namespace MiniPinterest.Web.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
