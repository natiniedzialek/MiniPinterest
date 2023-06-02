using MiniPinterest.Web.Data;
using MiniPinterest.Web.Models.Domain;

namespace MiniPinterest.Web.Repositories
{
    public class PinCommentRepository : IPinCommentRepository
    {
        private readonly MiniPinterestDbContext dbContext;

        public PinCommentRepository(MiniPinterestDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<PinComment> AddAsync(PinComment pinComment)
        {
            await dbContext.PinComments.AddAsync(pinComment);
            await dbContext.SaveChangesAsync();
            return pinComment;
        }
    }
}
