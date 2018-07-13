using PhotoManager.DAL.Entities;
using System.Web;

namespace PhotoManager.Services.PhotosHandler
{
    public interface IPhotoHandler
    {
        bool ReceivePhoto(HttpPostedFileBase file, Photo photo);
        void DeleteFile(string fileName);
    }
}
