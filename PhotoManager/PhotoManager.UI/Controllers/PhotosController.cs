using PhotoManager.DAL.Entities;
using PhotoManager.Services;
using System.Web.Mvc;
using PhotoManager.UI.Models.Photos;
using System.IO;
using System.Web;
using PhotoManager.UI.Models;
using System.Linq;
using Microsoft.AspNet.Identity;
using AutoMapper;
using System;
using System.Net;

namespace PhotoManager.UI.Controllers
{
    [Authorize]
    public class PhotosController : Controller
    {
        private IPhotoService _service;
        public PhotosController(IPhotoService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var photo = _service.GetPhoto(id);

            return View(photo);
        }

        [HttpGet]
        public ActionResult UserPhotos()
        {
            var userId = User.Identity.GetUserId();

            if (userId == null)
            {
                return RedirectToAction("GetAll");
            }
            var photos = _service.UserPhotos(userId);

            if (photos.Count == 0)
            {
                return View("EmptUserPhoto");
            }

            return View(photos);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetPhotosToAttach(int albumId)
        {
            var userId = User.Identity.GetUserId();

            if (userId == null)
            {
                return RedirectToAction("GetAll");
            }

            var photos = _service.UserPhotos(userId);

            var models = photos.ConvertAll(x => new AlbumPhotosModel()
            {
                AlbumId = albumId,
                PhotoId = x.Id,
                MediumSizeName = x.MediumSizeName
            });

            return View(models);
        }

        [HttpGet]
        public ActionResult Update(int photoId)
        {
            var userId = User.Identity.GetUserId();
            var photo = _service.GetPhoto(photoId);

            var photoModel = Mapper.Map<Photo, UpdatePhotoModel>(photo);

            ViewBag.Flash = photoModel.Flash;

            return View("Update",photoModel);
        }

        [HttpGet]
        public ActionResult Delete(int photoId)
        {
            _service.Delete(photoId);

            return RedirectToAction("UserPhotos");
        }
        
        [HttpPost]
        public ActionResult Create(CreatePhotoModel model, HttpPostedFileBase file)
        {
            var userId = User.Identity.GetUserId();

            if (userId == null)
            {
                return RedirectToAction("GetAll");
            }
            if (ModelState.IsValid)
            {
                // check image size
                if (Request.Files.Count > 0)
                {
                    file = Request.Files[0];
                    if (file != null && file.ContentLength > 0)
                    {
                        if (file.ContentLength <= 1048576)
                        {
                            var photo = Mapper.Map<CreatePhotoModel, Photo>(model);
                            photo.UserId = userId;

                            var status = _service.Add(photo, file);

                            if (string.IsNullOrWhiteSpace(status.ErrorMessage))
                            {
                                return RedirectToAction("UserPhotos");
                            }
                            else
                            {
                                ModelState.AddModelError("IsAnImage", status.ErrorMessage);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("Size", "Current image takes more than 1 MB");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("Empty", "Current image isn’t exist");
                    }
                }
            }
            var errors = ViewData.ModelState.Where(n => n.Value.Errors.Count > 0).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(UpdatePhotoModel model, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = User.Identity.GetUserId();

            var photo = Mapper.Map<UpdatePhotoModel, Photo>(model);
            photo.UserId = userId;

            file = Request.Files[0];
            if (file != null && file.ContentLength > 0)
            {
                if (Path.GetExtension(file.FileName).ToLower() == ".jpg"
                   || Path.GetExtension(file.FileName).ToLower() == ".jpeg")
                {
                    if (file.ContentLength <= 1048576)
                    {
                        _service.Update(photo, file);
                        return RedirectToAction("UserPhotos");
                    }
                    else
                    {
                        ModelState.AddModelError("Size", "Current image takes more than 1 MB");
                    }
                }
                else
                {
                    ModelState.AddModelError("IsAnImage", "Current file isn’t an image");
                }
            }
            else
            {
                _service.Update(photo);
                return RedirectToAction("UserPhotos");
            }

            return View(model);
        }
    }
}
