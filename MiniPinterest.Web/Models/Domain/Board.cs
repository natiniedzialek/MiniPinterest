﻿namespace MiniPinterest.Web.Models.Domain
{
    public class Board
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string ?Description { get; set; }
        public bool IsPublic { get ; set; }
        public ICollection<Pin> Pins { get; set; }
    }
}
