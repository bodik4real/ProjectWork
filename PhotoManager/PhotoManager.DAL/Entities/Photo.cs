using System;
using System.Collections.Generic;

namespace PhotoManager.DAL.Entities
{
    public class Photo
    {
        public Photo()
        {
            Likes = new List<Like>();
            Comments = new List<Comment>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string ActualSizeName { get; set; }
        public string MediumSizeName { get; set; }
        public string SmallSizeName { get; set; }
        public string ShutterSpeed { get; set; }
        public string Diaphragm { get; set; }
        public bool Flash { get; set; }

        public ICollection<Like> Likes { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
