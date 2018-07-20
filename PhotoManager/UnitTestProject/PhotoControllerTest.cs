using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoManager.DAL.Entities;
using PhotoManager.DAL.Identity;
using PhotoManager.Services;
using PhotoManager.UI.Controllers;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UnitTestProject
{
    [TestClass]
    public class PhotoControllerTest
    {
        private PhotosController _photoController;
        private Mock<ApplicationUserManager> _userManagerMock;
        private Mock<IPhotoService> _photoServiceMoq;
        private List<Photo> expectedData;

        [TestMethod]
        public void UserPhotosGetPhotosSuccessfullyTest()
        {
            expectedData = new List<Photo> { new Photo { Id = 5, Name = "cvfw" } };
            CreateControllerInstance(GenerateServiceMock(expectedData));
            var result = _photoController.UserPhotos() as ViewResult;
            var photos = (List<Photo>)result.ViewData.Model;

            Assert.AreEqual(photos, expectedData);
        }

        [TestMethod]
        public void UserPhotosGetEmptyListTest()
        {
            expectedData = new List<Photo>(0);
            CreateControllerInstance(GenerateServiceMock(expectedData));
            var result = _photoController.UserPhotos();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        private IPhotoService GenerateServiceMock(List<Photo> dataForMock)
        {
            _photoServiceMoq = new Mock<IPhotoService>();
            _photoServiceMoq.Setup(x => x.UserPhotos(It.IsAny<string>())).Returns(() => dataForMock);

            return _photoServiceMoq.Object;
        }

        private IController CreateControllerInstance(IPhotoService service)
        {
            //prepare identity for test
            var context = new Mock<HttpContextBase>();
            var identity = new GenericIdentity("BohdanTEST@gmail.com");
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "1"));
            var principal = new GenericPrincipal(identity, new[] { "user" });
            context.Setup(s => s.User).Returns(principal);

            // create new instance of controller and setup some value for it
            _photoController = new PhotosController(service);
            _photoController.ControllerContext = new ControllerContext(context.Object, new RouteData(), _photoController);

            return _photoController;
        }
    }
}
