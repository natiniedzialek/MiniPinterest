using Microsoft.EntityFrameworkCore;
using MiniPinterest.Web.Models.Domain;

namespace MiniPinterest.Web.Data
{
    public class MiniPinterestDbContext : DbContext
    {
        public MiniPinterestDbContext(DbContextOptions<MiniPinterestDbContext> options) : base(options)
        {
        }

        public DbSet<Pin> Pins { get; set; }
        public DbSet<Board> Boards { get; set; }
    }
}
