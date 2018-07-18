using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Hosting;

namespace PhotoManager.Services.PhotosHandler
{
    public class PhotosService : IPhotoService
    {
        private IPhotosRepository _repository;
        private IPhotoHandler _handler;
        public PhotosService(IPhotosRepository repository, IPhotoHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        public OperationStatus Add(Photo photo, HttpPostedFileBase file)
        {
            var status = new OperationStatus();
            var path = HostingEnvironment.MapPath(ConfigurationManager.AppSettings["UploadPath"]);

            try
            {
                if (IsFileValidImage(file))
                {

                    CreateNewNames(photo, file);

                    if (_handler.ReceivePhoto(file, photo, path))
                    {
                        _repository.Add(photo);
                    }
                    else
                    {
                        status.ErrorMessage = "Quality of current picture is very low";
                        return status;
                    }
                }
                else
                {
                    status.ErrorMessage = "Can’t upload current file like an image";
                    return status;
                }
            }
            catch (Exception ex)
            {
                status.ErrorMessage = "Current file isn’t an image";
                return status;
            }

            status.IsSuccessful = true;
            return status;
        }

        public Photo GetPhoto(int photoId)
        {
            return _repository.GetPhoto(photoId);
        }

        public List<Photo> UserPhotos(string userId)
        {
            return _repository.UserPhotos(userId);
        }

        public List<Photo> Search(string searchingString)
        {
            return _repository.Search(searchingString);
        }

        public List<Photo> AdvancedSearch(Photo searchingPhoto)
        {
            return _repository.AdvancedSearch(searchingPhoto);
        }

        public void Delete(int photoId)
        {
            _repository.Delete(photoId);
        }

        public void Update(Photo photo)
        {
            _repository.Update(photo);
        }

        public OperationStatus Update(Photo photo, HttpPostedFileBase file)
        {
            var status = new OperationStatus();
            var path = HostingEnvironment.MapPath(ConfigurationManager.AppSettings["UploadPath"]);

            if (file != null)
            {
                try
                {
                    if (IsFileValidImage(file))
                    {

                        DeleteOldImages(photo);
                        CreateNewNames(photo, file);

                        if (_handler.ReceivePhoto(file, photo, path))
                        {
                            _repository.Update(photo);
                        }
                        else
                        {
                            status.ErrorMessage = "Quality of current picture is very low";
                            return status;
                        }
                    }
                    else
                    {
                        status.ErrorMessage = "Can’t upload current file like an image";
                        return status;
                    }
                }
                catch (Exception ex)
                {
                    status.ErrorMessage = "Current file isn’t an image";
                    return status;
                }

            }
            status.IsSuccessful = true;
            return status;
        }

        public static bool IsFileValidImage(HttpPostedFileBase file)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                file.InputStream.CopyTo(ms);
                Image.FromStream(ms);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private void DeleteOldImages(Photo photo)
        {
            var path = HostingEnvironment.MapPath(ConfigurationManager.AppSettings["UploadPath"]);

            _handler.DeleteFile(Path.Combine(path, photo.ActualSizeName));
            _handler.DeleteFile(Path.Combine(path, photo.MediumSizeName));
            _handler.DeleteFile(Path.Combine(path, photo.SmallSizeName));
        }

        private void CreateNewNames(Photo photo, HttpPostedFileBase file)
        {
            photo.ActualSizeName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            photo.MediumSizeName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            photo.SmallSizeName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        }
    }
}