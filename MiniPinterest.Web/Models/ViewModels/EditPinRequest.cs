﻿using MiniPinterest.Web.Models.Domain;

namespace MiniPinterest.Web.Models.ViewModels
{
    public class EditPinRequest
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsPublic { get; set; }
        public ICollection<Board> Boards { get; set; }
    }
}
