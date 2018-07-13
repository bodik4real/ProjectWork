using PhotoManager.DAL.Entities;


namespace PhotoManager.Services
{
    public interface ILikeService
    {
        OperationStatus Like(Like like);
        OperationStatus UnLike(string userId, int photoId);
        bool IsLikeExist(string userId, int phototId);
        int GetLikesCount(int photoId);
    }
}
