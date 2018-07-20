using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoManager.DAL.Entities;
using PhotoManager.DAL.Identity;
using PhotoManager.Services;
using PhotoManager.UI.Controllers;
using PhotoManager.UI.Models.Photos;
using System;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UnitTestProject
{
    // TODO: TEST CLASS Attribute!
    class LikeControllerTest
    {
        private LikesController _likeController;
        private Mock<ILikeService> _likeService;
        private Like _model;
        private ApplicationUserManager _userManager;
        private Mock<ApplicationUserManager> _userManagerMock;

        [TestInitialize]
        public void Initialize()
        {
            _likeService = new Mock<ILikeService>();
            _model = new Like();
            _likeService.Setup(x => x.Like(_model)).Returns(() => new OperationStatus { IsSuccessful = true, ErrorMessage = "vwgnfwol" });

            var context = new Mock<HttpContextBase>();

            var identity = new GenericIdentity("dominik.ernst@xyz123.de");
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "1"));
            var principal = new GenericPrincipal(identity, new[] { "user" });
            context.Setup(s => s.User).Returns(principal);

            _likeController = new LikesController(_userManager, _likeService.Object);
          //  _likeController.ControllerContext = new ControllerContext(context.Object, new RouteData(), _likeController);


        }

        //[TestMethod]
        //public void TestLikeMethod()
        //{
        //    var result = _likeController.Like(new LikeModel { photoId = 1024 });
        //    Assert.AreEqual(result, HttpStatusCode.OK);
        //}
    }
}
