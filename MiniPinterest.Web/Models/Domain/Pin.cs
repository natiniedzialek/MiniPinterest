namespace MiniPinterest.Web.Models.Domain
{
    public class Pin
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string ?Description { get; set; }
        public string ImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate {  get; set; }
        public Guid AuthorID { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
