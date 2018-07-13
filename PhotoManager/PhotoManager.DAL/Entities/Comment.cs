using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoManager.DAL.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int PhotoId { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Text { get; set; }
        public string UserName { get; set; }
    }
}
