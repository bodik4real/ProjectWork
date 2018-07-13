using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoManager.Services;
using PhotoManager.UI.Controllers;

namespace UnitTestProject
{
    [TestClass]
    public class PhotoControllerTest
    {
        IPhotoService service;
        [TestMethod]
        public void UserPhotosResultNotNull()
        {
            var controller = new PhotosController(service);
            var result = controller

        }
    }
}
