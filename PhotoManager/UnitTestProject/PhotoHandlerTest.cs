using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoManager.DAL.Entities;
using PhotoManager.Services.PhotosHandler;
using Moq;
using System.Web;

namespace UnitTestProject
{
    [TestClass]
    public class PhotoHandlerTest
    {
        private PhotoHandler _subject;
      
        [TestInitialize]
        public void Initialize()
        {
            _subject = new PhotoHandler();

        }

        [TestMethod]
        public void TestIsMethodCalled()
        {
            //Mock photo = new Mock<Photo>();
            //Mock file = new Mock<HttpPostedFileBase>();

            //_subject.ReceivePhoto(file, photo)
            var photo = new Photo();

            var file = new PhotoFileMock();
            var result =  _subject.ReceivePhoto(file, photo);

            Assert.IsTrue(result);

        }
 
    }
    public class PhotoFileMock : HttpPostedFileBase
    {

    }
}
