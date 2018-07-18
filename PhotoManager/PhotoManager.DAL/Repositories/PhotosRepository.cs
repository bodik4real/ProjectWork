using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Configuration;
using System.Web.Hosting;
using System.Data.Entity;

namespace PhotoManager.DAL.Repositories
{
    public class PhotosRepository : IPhotosRepository
    {
        private PhotoManagerDbContext _context;
        public PhotosRepository(PhotoManagerDbContext context)
        {
            _context = context;
        }
        public List<Photo> UserPhotos(string userId)
        {
            return (from item in _context.Photos
                    where item.UserId == userId
                    select item)
                    .ToList();
        }

        public Photo GetPhoto(int photoId)
        {
            var photo = _context.Photos
                .Include(x => x.Comments)
                .Where(x => x.Id == photoId)
                .FirstOrDefault();

            return photo;
        }

        public void Add(Photo photo)
        {
            _context.Photos.Add(photo);

            _context.SaveChanges();
        }

        public void Update(Photo photo)
        {
            var updatedPhoto = (from a in _context.Photos
                                where a.Id == photo.Id
                                select a)
                                .FirstOrDefault();

            if (updatedPhoto == null)
            {
                return;
            }

            updatedPhoto.Name = photo.Name;
            updatedPhoto.Description = photo.Description;
            updatedPhoto.Date = photo.Date;
            updatedPhoto.Flash = photo.Flash;
            updatedPhoto.Diaphragm = photo.Diaphragm;
            updatedPhoto.ShutterSpeed = photo.ShutterSpeed;

            if (!string.IsNullOrWhiteSpace(photo.MediumSizeName) || !string.IsNullOrWhiteSpace(photo.SmallSizeName) || !string.IsNullOrWhiteSpace(photo.ActualSizeName))
            {
                updatedPhoto.ActualSizeName = photo.ActualSizeName;
                updatedPhoto.MediumSizeName = photo.MediumSizeName;
                updatedPhoto.SmallSizeName = photo.SmallSizeName;
            }

            _context.SaveChanges();
        }

        public List<Photo> Search(string searchingString)
        {
            var param = new SqlParameter("@searchingString", searchingString);
            var photos = _context.Photos.SqlQuery("Search @searchingString", param).ToList();

            return photos;
        }

        public List<Photo> AdvancedSearch(Photo searchingPhoto)
        {
            var query = _context.Photos.AsQueryable();
            var photos = new List<Photo>();

            if (!string.IsNullOrWhiteSpace(searchingPhoto.Name))
            {
                query = (query.Where(x => x.Name.Contains(searchingPhoto.Name)));
            }
            if (!string.IsNullOrWhiteSpace(searchingPhoto.Description))
            {
                query = query.Where(x => x.Description.Contains(searchingPhoto.Description));
            }
            if (!string.IsNullOrWhiteSpace(searchingPhoto.ShutterSpeed))
            {
                query = query.Where(x => x.ShutterSpeed.Contains(searchingPhoto.ShutterSpeed));
            }
            if (!string.IsNullOrWhiteSpace(searchingPhoto.Diaphragm))
            {
                query = query.Where(x => x.Diaphragm.Contains(searchingPhoto.Diaphragm));
            }

            //Q in any case has Flash value. It doesn’t make sense to check it. if check in some cases we have lost data. 
            //(when we have only name and value of this field for example). 
            int flash = Convert.ToInt32(searchingPhoto.Flash);

            if (flash == 1)
            {
                query = query.Where(x => x.Flash == searchingPhoto.Flash);
            }
            else
            {
                query = query.Where(x => x.Flash == true || x.Flash == false);
            }
            return query.ToList();
        }

        public bool Delete(int photoId)
        {
            var photoToDelete = _context.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photoToDelete == null)
            {
                return false;
            }

            DeleteCurrPhotoAsAlbumTitle(photoToDelete.MediumSizeName);

            DeleteAlbumPhoto(photoId);

            DeleteLikes(photoId);

            DeleteComments(photoId);

            DeletePhysically(photoToDelete);

            _context.Photos.Remove(photoToDelete);

            _context.SaveChanges();

            return true;
        }

        private void DeletePhysically(Photo photo)
        {
            var path = HostingEnvironment.MapPath(ConfigurationManager.AppSettings["UploadPath"]);
            File.Delete(Path.Combine(path, photo.SmallSizeName));
            File.Delete(Path.Combine(path, photo.MediumSizeName));
            File.Delete(Path.Combine(path, photo.ActualSizeName));
        }

        private void DeleteAlbumPhoto(int photoId)
        {
            var albumPhotosForDelete = (from p in _context.AlbumPhotos
                                        where p.PhotoId == photoId
                                        select p).ToList();

            if (albumPhotosForDelete.Any())
            {
                _context.AlbumPhotos.RemoveRange(albumPhotosForDelete);
            }
        }

        private void DeleteCurrPhotoAsAlbumTitle(string MediumSizeName)
        {
            var albumsWithTitle = _context.Albums.Where(x => x.AlbumTitle == MediumSizeName).ToList();

            if (albumsWithTitle.Any())
            {
                foreach (var album in albumsWithTitle)
                {
                    album.AlbumTitle = string.Empty;
                }
            }
        }
        private void DeleteLikes(int photoId)
        {
            var likesForDelete = _context.Likes.Where(x => x.PhotoId == photoId).ToList();
            _context.Likes.RemoveRange(likesForDelete);

        }
        private void DeleteComments(int photoId)
        {
            var commentsForDelete = _context.Comments.Where(x => x.PhotoId == photoId).ToList();
            _context.Comments.RemoveRange(commentsForDelete);
        }
    }
}
