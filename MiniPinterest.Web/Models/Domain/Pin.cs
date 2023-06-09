﻿namespace MiniPinterest.Web.Models.Domain
{
    public class Pin
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsPublic { get; set; }

        public ICollection<Board> Boards { get; set; }
        public ICollection<PinLike> Likes { get; set; }
        public ICollection<PinComment> Comments { get; set; }
    }
}
