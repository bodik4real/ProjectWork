using System;
using System.Collections.Generic;

namespace PhotoManager.DAL.Entities
{
    public class Album
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public ICollection<AlbumPhoto> AlbumPhotos { get; set; }
        public string AlbumTitle { get; set; }
    }
}