using PhotoManager.DAL.Entities;
using System.Web;

namespace PhotoManager.Services.PhotosHandler
{
    public interface IPhotoHandler
    {
        bool ReceivePhoto(HttpPostedFileBase file, Photo photo, string path);
        bool DeleteFile(string fileName);
    }
}
