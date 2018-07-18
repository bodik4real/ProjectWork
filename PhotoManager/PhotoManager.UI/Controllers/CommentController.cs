using PhotoManager.DAL.Entities;
using PhotoManager.UI.Models.Photos;
using System;
using System.Web.Http;
using PhotoManager.DAL.Identity;
using PhotoManager.Services;
using Microsoft.AspNet.Identity;
using System.Net.Http;
using System.Net;

namespace PhotoManager.UI.Controllers
{
    [Authorize]
    public class CommentController : ApiController
    {
        private ApplicationUserManager _userManager;
        private ICommentService _service;

        public CommentController(ApplicationUserManager userManager, ICommentService service)
        {
            _userManager = userManager;
            _service = service;
        }

        [HttpGet]
        public HttpResponseMessage GetAll(int photoId)
        {
            var comments = _service.GetAll(photoId);
            var response = Request.CreateResponse(HttpStatusCode.OK, comments);
            return response;
        }

        [HttpPost]
        public IHttpActionResult AddComment([FromBody]CommentModel model)
        {
            var userId = User.Identity.GetUserId();
            var userName = User.Identity.GetUserName();

            var comment = new Comment();

            comment.PhotoId = model.photoId;
            comment.Text = model.Text;
            comment.Date = DateTime.Now;
            comment.UserId = userId;
            comment.UserName = userName;

            _service.AddComment(comment);

            return Ok();
        }
    }
}