namespace MiniPinterest.Web.Models.Domain
{
    public class PinComment
    {
        public Guid Id { get; set; }
        public Guid PinId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; }
    }
}
