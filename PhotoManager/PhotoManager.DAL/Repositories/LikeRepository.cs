using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.Entities;
using System.Linq;

namespace PhotoManager.DAL.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private PhotoManagerDbContext _context;
        public LikeRepository(PhotoManagerDbContext context)
        {
            _context = context;
        }
        public void Like(Like like)
        {
            _context.Likes.Add(like);

            _context.SaveChanges();
        }
        public void UnLike(string userId, int photoId)
        {
            var likeForRemove = _context.Likes
            .Where(x => x.PhotoId == photoId && x.UserId == userId)
            .FirstOrDefault();

            if (likeForRemove != null)
            {
                _context.Likes.Remove(likeForRemove);
                _context.SaveChanges();
            }
        }
        public int GetLikesCount(int photoId)
        {
            var likes =  _context.Likes.Where(x => x.PhotoId == photoId).ToList();
            return likes.Count;
        }
        public bool IsLikeExist(string userId, int phototId)
        {
            return _context.Likes.Any(x => x.PhotoId == phototId && x.UserId == userId);
        }

    }
}
