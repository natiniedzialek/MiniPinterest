namespace MiniPinterest.Web.Models.ViewModels
{
    public class AddBoardRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
    }
}
