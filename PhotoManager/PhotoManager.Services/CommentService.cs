using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.Entities;
using System.Collections.Generic;

namespace PhotoManager.Services
{
    public class CommentService : ICommentService
    {
        private ICommentRepository _repository;
        public CommentService(ICommentRepository repository)
        {
            _repository = repository;
        }
        public void AddComment(Comment comment)
        {
            _repository.AddComment(comment);
        }
        public List<Comment> GetAll(int photoId)
        {
            return _repository.GetAll(photoId);
        }
    }
}
