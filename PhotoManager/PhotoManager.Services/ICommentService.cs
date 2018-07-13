using PhotoManager.DAL.Entities;
using System.Collections.Generic;

namespace PhotoManager.Services
{
    public interface ICommentService
    {
        void AddComment(Comment comment);
        List<Comment> GetAll(int photoId);
    }
}
