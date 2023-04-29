namespace MiniPinterest.Web.Models.Domain
{
    public class Pin
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public string Title { get; set; }
        public string ?Description { get; set; }
        public string ImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate {  get; set; }
        public ICollection<Board> Boards { get; set; }
    }
}
