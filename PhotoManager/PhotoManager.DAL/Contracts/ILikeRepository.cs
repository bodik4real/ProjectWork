using PhotoManager.DAL.Entities;

namespace PhotoManager.DAL.Contracts
{
    public interface ILikeRepository
    {
        void Like(Like like);
        void UnLike(string userId, int photoId);
        bool IsLikeExist(string userId, int phototId);
        int GetLikesCount(int photoId);
    }
}
