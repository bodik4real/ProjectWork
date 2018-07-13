using PhotoManager.DAL.Entities;
using System.Collections.Generic;

namespace PhotoManager.Services
{
    public interface IAlbumsService
    {
        Album GetAlbum(int albumId);
        List<Album> GetAll();
        List<Album> UserAlbums(string userId);
        OperationStatus Add(Album album);
        OperationStatus Update(Album album);
        void Delete(int albumId);
        void Attach(List<int> attachments, int albumId);
        Album GetPhotosByAlbumName(string albumName);
        Album GetPhotosByAlbumId(int albumId);
        void Detach(int photoId, int albumId);
       void SetAsTitle(string albumTitle, int albumId);
    }
}
