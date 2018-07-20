using PhotoManager.DAL.Entities;
using PhotoManager.DAL.Identity;
using PhotoManager.Services;
using System.Web.Http;
using PhotoManager.UI.Models.Photos;
using Microsoft.AspNet.Identity;
using System.Net.Http;
using System.Net;

namespace PhotoManager.UI.Controllers
{
    [Authorize]
    public class LikesController : ApiController
    {
        private ApplicationUserManager _userManager;
        private ILikeService _service;

        public LikesController(ApplicationUserManager userManager, ILikeService service)
        {
            _userManager = userManager;
            _service = service;
        }

        [HttpGet]
        public HttpResponseMessage GetLikesCount(int photoId)
        {
            var likes = _service.GetLikesCount(photoId);
            var response = Request.CreateResponse(HttpStatusCode.OK, likes);
            return response;
        }

        [HttpPost]
        [Route("api/Likes/UnLike")]
        public IHttpActionResult UnLike([FromBody]LikeModel model)
        {
            var userId = User.Identity.GetUserId();

            if (_service.IsLikeExist(userId, model.photoId))
            {
                _service.UnLike(userId, model.photoId);
            }
            return Ok();
        }

        [HttpPost]
        [Route("api/Likes/Like")]
        public IHttpActionResult Like([FromBody]LikeModel model)
        {
            var userId = User.Identity.GetUserId();

            var like = new Like();
            like.PhotoId = model.photoId;
            like.UserId = userId;

            var status = _service.Like(like);
            //if (!string.IsNullOrWhiteSpace(status.ErrorMessage))
            //{
            //    ModelState.AddModelError("Already Liked", status.ErrorMessage);
            //}
            return Ok();
        }
    }
}