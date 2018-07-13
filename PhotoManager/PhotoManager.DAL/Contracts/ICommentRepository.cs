using PhotoManager.DAL.Entities;
using System.Collections.Generic;

namespace PhotoManager.DAL.Contracts
{
    public interface ICommentRepository
    {
        void AddComment(Comment comment);
        List<Comment> GetAll(int photoId);
    }
}
