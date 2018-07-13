
namespace PhotoManager.DAL.Entities
{
    public class Like
    {
        public int Id { get; set; }
        public int PhotoId { get; set; }
        public string UserId { get; set; }
    }
}
