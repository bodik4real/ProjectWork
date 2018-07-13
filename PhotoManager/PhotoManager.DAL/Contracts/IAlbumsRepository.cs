using PhotoManager.DAL.Entities;
using System.Collections.Generic;

namespace PhotoManager.DAL.Contracts
{
    public interface IAlbumsRepository
    {
        List<Album> GetAll();
        List<Album> UserAlbums(string userId);
        void Add(Album album);
        void Delete(int albumId);
        void Update(Album album);
        Album GetAlbum(int albumId);
        bool IsAlbumExist(string albumName, string userId);
        void Attach(List<int> attachments, int albumId);
        Album GetPhotosByAlbumName(string albumName);
        Album GetPhotosByAlbumId(int albumId);
        bool Detach(int photoId, int albumId);
        void SetAsTitle(string albumTitle, int albumId);
    }
}
