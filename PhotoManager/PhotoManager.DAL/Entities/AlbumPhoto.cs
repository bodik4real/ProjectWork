
namespace PhotoManager.DAL.Entities
{
    public class AlbumPhoto
    {
        public int Id { get; set; }
        public int AlbumId { get; set; }
        public int PhotoId { get; set; }
        public Photo Photo { get; set; }

    }
}
