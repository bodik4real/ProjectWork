using AutoMapper;
using Microsoft.AspNet.Identity;
using PhotoManager.DAL.Entities;
using PhotoManager.Services;
using PhotoManager.UI.Models;
using PhotoManager.UI.Models.Albums;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PhotoManager.UI.Controllers
{
    [Authorize]
    public class AlbumsController : Controller
    {
        private IAlbumsService _service;
        public AlbumsController(IAlbumsService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetAll()
        {
            return View(_service.GetAll());
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetPhotosByAlbumId(int albumId)
        {
            var album = _service.GetPhotosByAlbumId(albumId);

            return View(album);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetPhotosByAlbumName(string albumName)
        {
            var albumContent = _service.GetPhotosByAlbumName(albumName);

            return View("GetPhotosByAlbumId", albumContent);
        }

        [HttpGet]
        public ActionResult UserAlbums()
        {
            var userId = User.Identity.GetUserId();

            if (userId == null)
            {
                return RedirectToAction("GetAll");
            }

            var albums = _service.UserAlbums(userId);

            if (albums.Count == 0)
            {
                return View("JustRegisteredUser");
            }

            return View(albums);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Update(int albumId)
        {
            var album = _service.GetAlbum(albumId);

            UpdateAlbumModel albumModel = Mapper.Map<Album, UpdateAlbumModel>(album);

            return View(albumModel);
        }

        [HttpGet]
        public ActionResult Detach(int photoId, int albumId)
        {
            _service.Detach(photoId, albumId);
            return RedirectToAction("GetPhotosByAlbumId", new { albumId = albumId });
        }

        [HttpGet]
        public ActionResult SetAsTitle(string albumTitle, int albumId)
        {
            _service.SetAsTitle(albumTitle, albumId);
            return RedirectToAction("UserAlbums");
        }

        [HttpGet]
        public ActionResult Delete(int albumId)
        {
            _service.Delete(albumId);

            return RedirectToAction("UserAlbums");
        }

        [HttpPost]
        public ActionResult Create(CreateAlbumModel model)
        {
            var userId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                var album = Mapper.Map<CreateAlbumModel, Album>(model);
                album.UserId = userId;

                var status = _service.Add(album);

                if (string.IsNullOrWhiteSpace(status.ErrorMessage))
                {
                    return RedirectToAction("UserAlbums");
                }
                else
                {
                    ModelState.AddModelError("CustomEror", status.ErrorMessage);
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(UpdateAlbumModel model)
        {
            var oldAlbum = _service.GetAlbum(model.Id);

            if (ModelState.IsValid)
            {
                var album = Mapper.Map<UpdateAlbumModel, Album>(model);
                album.UserId = oldAlbum.UserId;

                _service.Update(album);

                return RedirectToAction("UserAlbums");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult AddPhotos(List<AlbumPhotosModel> model)
        {
            if (model!= null && model.Any() && ModelState.IsValid)
            {
                var albumId = model.FirstOrDefault().AlbumId;
                var attachments = new List<int>();

                foreach (var m in model)
                {
                    if (m.IsChecked)
                    {
                        attachments.Add(m.PhotoId);
                    }
                }

                _service.Attach(attachments, albumId);
            }
            else
            {
                return View(model);
            }

            return RedirectToAction("UserAlbums", "Albums");
        }

    }
}