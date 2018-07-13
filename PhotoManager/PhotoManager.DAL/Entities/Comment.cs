using System;

namespace PhotoManager.DAL.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int PhotoId { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Text { get; set; }
        public string UserName { get; set; }
    }
}
