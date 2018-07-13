using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PhotoManager.DAL.Repositories
{
    public class CommentRepository: ICommentRepository
    {
        private PhotoManagerDbContext _context;
        public CommentRepository(PhotoManagerDbContext context)
        {
            _context = context;
        }
        public void AddComment(Comment comment)
        {
            _context.Comments.Add(comment);

            _context.SaveChanges();
        }
        public List<Comment> GetAll(int photoId)
        {
            return _context.Comments.Where(x => x.PhotoId == photoId).OrderByDescending(x=>x.Date).ToList();
        }
    }
}
