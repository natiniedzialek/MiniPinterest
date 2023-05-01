namespace MiniPinterest.Web.Models.ViewModels
{
    public class AddPinRequest
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPublic { get; set; }
    }
}
