using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.Entities;
using System.Collections.Generic;

namespace PhotoManager.Services
{

    public class AlbumsService : IAlbumsService
    {
        private IAlbumsRepository _repository;

        public AlbumsService(IAlbumsRepository repository)
        {
            _repository = repository;
        }

        public List<Album> GetAll()
        {
            return _repository.GetAll();
        }

        public List<Album> UserAlbums(string userId)
        {
            return _repository.UserAlbums(userId);
        }

        public OperationStatus Add(Album album)
        {
            var status = new OperationStatus();

            if (_repository.IsAlbumExist(album.Name, album.UserId))
            {
                status.ErrorMessage = "Album with such name is already exists";

                return status;
            }

            _repository.Add(album);

            status.IsSuccessful = true;
            return status;
        }
        public OperationStatus Update(Album album)
        {
            var status = new OperationStatus();

            if (_repository.IsAlbumExist(album.Name, album.UserId))
            {
                status.ErrorMessage = "Album with such name is already exists";

                return status;
            }

            _repository.Update(album);

            status.IsSuccessful = true;
            return status;
        }

        public void Delete(int albumId)
        {
            _repository.Delete(albumId);
        }

        public Album GetAlbum(int albumId)
        {
            return _repository.GetAlbum(albumId);
        }

        public void SetAsTitle(string albumTitle, int albumId)
        {
            _repository.SetAsTitle(albumTitle, albumId);
        }

        public void Attach(List<int> attachments, int albumId)
        {
            _repository.Attach(attachments, albumId);
        }

        public Album GetPhotosByAlbumName(string albumName)
        {
            return _repository.GetPhotosByAlbumName(albumName);
        }

        public Album GetPhotosByAlbumId(int albumId)
        {
            return _repository.GetPhotosByAlbumId(albumId);
        }
        public void Detach(int photoId, int albumId)
        {
            _repository.Detach(photoId, albumId);
        }
    }
}
