using PhotoManager.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Web;

namespace PhotoManager.Services
{
    public interface IPhotoService
    {
        OperationStatus Add(Photo photo, HttpPostedFileBase file);
        List<Photo> UserPhotos(string userId);
        void Update(Photo photo);
        OperationStatus Update(Photo photo, HttpPostedFileBase file);
        void Delete(int photoId);
        Photo GetPhoto(int photoId);
        List<Photo> Search(string searchingString);
        List<Photo> AdvancedSearch(Photo searchingPhoto);
       
    }
}
