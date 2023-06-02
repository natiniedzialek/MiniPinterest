using Microsoft.EntityFrameworkCore;

namespace MiniPinterest.Web.Models.Domain
{
    public class PinLike
    {
        public Guid Id { get; set; }
        public Guid PinId { get; set; }
        public Guid UserId { get; set; }
    }
}
