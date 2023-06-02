namespace MiniPinterest.Web.Models.ViewModels
{
    public class AddLikeRequest
    {
        public Guid UserId { get; set; }
        public Guid PinId { get; set; }
    }
}
