using MiniPinterest.Web.Models.Domain;

namespace MiniPinterest.Web.Models.ViewModels
{
    public class EditBoardRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsPublic { get; set; }
        public ICollection<Pin>? Pins { get; set; }
    }
}
