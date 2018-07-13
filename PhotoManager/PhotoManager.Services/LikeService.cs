using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.Entities;

namespace PhotoManager.Services
{
    public class LikeService : ILikeService
    {
        private ILikeRepository _repository;
        public LikeService(ILikeRepository repository)
        {
            _repository = repository;
        }
        public OperationStatus Like(Like like)
        {

            var status = new OperationStatus();

            if (!_repository.IsLikeExist(like.UserId, like.PhotoId))
            {
                _repository.Like(like);
            }
            else
            {
                status.ErrorMessage = "Like is already exist";
                return status;
            }

            status.IsSuccessful = true;
            return status;
        }
        public OperationStatus UnLike(string userId, int photoId)
        {
            var status = new OperationStatus();
            if (_repository.IsLikeExist(userId, photoId))
            {
                _repository.UnLike(userId, photoId);
            }
            else
            {
                status.ErrorMessage = "Access denied. You used all Your attempts!";
                return status;
            }

            status.IsSuccessful = true;
            return status;
        }
        public bool IsLikeExist(string userId, int phototId)
        {
            return _repository.IsLikeExist(userId, phototId);
        }
        public int GetLikesCount(int photoId)
        {
            return _repository.GetLikesCount(photoId);
        }
    }
}
   
