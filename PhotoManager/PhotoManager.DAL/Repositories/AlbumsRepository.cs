using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace PhotoManager.DAL.Repositories
{
    public class AlbumsRepository : IAlbumsRepository
    {
        private PhotoManagerDbContext _context;
        public AlbumsRepository(PhotoManagerDbContext context)
        {
            _context = context;
        }

        public List<Album> GetAll()
        {
            return _context.Albums.ToList();
        }

        public List<Album> UserAlbums(string userId)
        {
            return (from item in _context.Albums
                    where item.UserId == userId
                    select item)
                   .ToList();
        }

        public Album GetAlbum(int albumId)
        {
            return (from a in _context.Albums
                    where a.Id == albumId
                    select a).FirstOrDefault();
        }
        public Album GetPhotosByAlbumId(int albumId)
        {
            var album = _context.Albums
                .Include(x => x.AlbumPhotos)
                .Include(x => x.AlbumPhotos.Select(a => a.Photo))
                .Include(x => x.AlbumPhotos.Select(a => a.Photo.Likes))
                .Where(x => x.Id == albumId)
                .FirstOrDefault();

            return album;
        }

        public Album GetPhotosByAlbumName(string albumName)
        {
            var album = _context.Albums
                        .Include(x => x.AlbumPhotos)
                        .Include(x => x.AlbumPhotos.Select(a => a.Photo))
                        .Include(x => x.AlbumPhotos.Select(a => a.Photo.Likes))
                        .Where(x => x.Name == albumName)
                        .FirstOrDefault();

            return album;
        }

        public void Attach(List<int> attachments, int albumId)
        {

            foreach (var photoId in attachments)
            {
                var photoAlbum = new AlbumPhoto();
                photoAlbum.AlbumId = albumId;
                photoAlbum.PhotoId = photoId;
                _context.AlbumPhotos.Add(photoAlbum);
            }
            _context.SaveChanges();
        }

        public void Add(Album album)
        {
            _context.Albums.Add(album);

            _context.SaveChanges();
        }

        public bool IsAlbumExist(string albumName, string userId)
        {
            return _context.Albums.Any(x => x.Name == albumName && x.UserId == userId);
        }

        public void Update(Album album)
        {
            var updatedAlbum = (from a in _context.Albums
                                where a.Id == album.Id
                                select a)
                                .FirstOrDefault();

            if (updatedAlbum == null)
            {
                return;
            }

            updatedAlbum.Name = album.Name;
            updatedAlbum.Description = album.Description;
            updatedAlbum.Date = album.Date;

            _context.SaveChanges();
        }


        public void SetAsTitle(string albumTitle, int albumId)
        {
            var album = _context.Albums
                .Where(x => x.Id == albumId)
                .FirstOrDefault();

            album.AlbumTitle = albumTitle;

            _context.SaveChanges();
        }

        public void Delete(int albumId)
        {
            var AlbumForDelete = (from a in _context.Albums
                                  where a.Id == albumId
                                  select a)
                                  .FirstOrDefault();

            if (AlbumForDelete == null)
            {
                return;
            }

            var albumphotoForDelete = (from p in _context.AlbumPhotos
                                       where p.AlbumId == albumId
                                       select p).ToList();

            _context.Albums.Remove(AlbumForDelete);

            _context.AlbumPhotos.RemoveRange(albumphotoForDelete);

            _context.SaveChanges();
        }
        public bool Detach(int photoId, int albumId)
        {

            var album = _context.Albums.Where(a => a.Id == albumId).FirstOrDefault();
            if (album == null)
            {
                return false;
            }
            var photo = _context.Photos.Where(a => a.Id == photoId).FirstOrDefault();

            if (photo == null)
            {
                return false;
            }

            if (album.AlbumTitle == photo.MediumSizeName)
                album.AlbumTitle = string.Empty;

            var albumPhotoForRemove = (from p in _context.AlbumPhotos
                                       where p.PhotoId == photoId && p.AlbumId == albumId
                                       select p)
                                       .FirstOrDefault();
            if (albumPhotoForRemove == null)
            {
                return false;
            }

            _context.AlbumPhotos.Remove(albumPhotoForRemove);

            _context.SaveChanges();

            return true;
        }
    }
}
