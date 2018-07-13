using PhotoManager.DAL.Entities;
using System.Collections.Generic;

namespace PhotoManager.DAL.Contracts
{
    public interface IPhotosRepository
    {
        void Add(Photo photo);
        List<Photo> UserPhotos(string userId);
        void Update(Photo photo);
        bool Delete(int photoId);
        Photo GetPhoto(int photoId);
        List<Photo> Search(string searchingString);
        List<Photo> AdvancedSearch(Photo searchingPhoto);
    }
}
