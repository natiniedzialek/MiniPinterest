using Microsoft.EntityFrameworkCore;
using MiniPinterest.Web.Data;
using MiniPinterest.Web.Models.Domain;

namespace MiniPinterest.Web.Repositories
{
    public class PinRepository : IPinRepository
    {
        private readonly MiniPinterestDbContext miniPinterestDbContext;

        public PinRepository(MiniPinterestDbContext miniPinterestDbContext)
        {
            this.miniPinterestDbContext = miniPinterestDbContext;
        }

        public async Task<Pin> AddAsync(Pin pin)
        {
            await miniPinterestDbContext.Pins.AddAsync(pin);
            await miniPinterestDbContext.SaveChangesAsync();
            return pin;
        }

        public Task<Pin?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Pin>> GetAllAsync()
        {
            return await miniPinterestDbContext.Pins.ToListAsync();
        }

        public Task<Pin?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Pin?> UpdateAsync(Pin pin)
        {
            throw new NotImplementedException();
        }
    }
}
