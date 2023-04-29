namespace MiniPinterest.Web.Models.Domain
{
    public class Board
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string ?Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsPublic { get ; set; }
        public ICollection<Pin> ?Pins { get; set; }
        
        public Board(string name, string? description, bool isPublic)
        {
            Id = Guid.NewGuid();
            UserId = Guid.NewGuid();
            Name = name;
            Description = description;
            CreatedAt = DateTime.Now;
            IsPublic = isPublic;
        }

        public Board(Guid Id, Guid UserId, string name, string description, DateTime createdAt, bool isPublic, ICollection<Pin> pins)
        {
            this.Id = Id;
            this.UserId = UserId;
            this.Name = name;
            this.Description = description;
            this.CreatedAt = createdAt;
            this.IsPublic = isPublic;
            this.Pins = pins;
        }
    }
}
