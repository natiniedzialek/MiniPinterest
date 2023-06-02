using Microsoft.EntityFrameworkCore;
using MiniPinterest.Web.Data;
using MiniPinterest.Web.Models.Domain;

namespace MiniPinterest.Web.Repositories
{
    public class PinLikeRepository : IPinLikeRepository
    {
        private readonly MiniPinterestDbContext dbContext;

        public PinLikeRepository(MiniPinterestDbContext dbContext) 
        { 
            this.dbContext = dbContext;
        }

        public async Task<int> GetTotalLikes(Guid pinId)
        {
            int nLikes = await dbContext.PinLikes.CountAsync(x => x.PinId == pinId);

            return nLikes;
        }

        public async Task<PinLike> AddLikeAsync(PinLike pinLike)
        {
            await dbContext.PinLikes.AddAsync(pinLike);
            await dbContext.SaveChangesAsync();
            return pinLike;
        }
    }
}
