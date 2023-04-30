using Microsoft.EntityFrameworkCore;
using MiniPinterest.Web.Data;
using MiniPinterest.Web.Models.Domain;
using System.Net.NetworkInformation;

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

        public async Task<Pin?> DeleteAsync(Guid id)
        {
            Pin existingPin = await miniPinterestDbContext.Pins.FindAsync(id);

            if (existingPin != null)
            {
                miniPinterestDbContext.Pins.Remove(existingPin);
                await miniPinterestDbContext.SaveChangesAsync();
            }

            return existingPin;
        }

        public async Task<IEnumerable<Pin>> GetAllAsync()
        {
            return await miniPinterestDbContext
                .Pins
                .Include(x => x.Boards)
                .ToListAsync();
        }

        public async Task<Pin?> GetByIdAsync(Guid id)
        {
            return await miniPinterestDbContext
                .Pins
                .Include(x => x.Boards)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Pin?> UpdateAsync(Pin pin)
        {
            Pin ?existingPin = await miniPinterestDbContext
                .Pins
                .Include(x => x.Boards)
                .FirstOrDefaultAsync(x => x.Id == pin.Id);

            if(existingPin != null)
            {
                existingPin.Title = pin.Title;
                existingPin.Description = pin.Description;
                existingPin.UrlHandle = pin.UrlHandle;
                existingPin.IsPublic = pin.IsPublic;
                existingPin.Boards = pin.Boards;

                await miniPinterestDbContext.SaveChangesAsync();
                return existingPin;
            }

            return null;
        }
    }
}
