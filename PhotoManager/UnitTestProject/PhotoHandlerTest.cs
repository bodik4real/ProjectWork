using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoManager.Services.PhotosHandler;
using Moq;
using System.Web;
using System.IO;
using System.Configuration;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using PhotoManager.DAL.Entities;

namespace UnitTestProject
{
    [TestClass]
    public class PhotoHandlerTest
    {
        private PhotoHandler _handler;

        [TestInitialize]
        public void Initialize()
        {
            _handler = new PhotoHandler();
        }

        [TestMethod]
        public void TestDeleteFile()
        {
            var path = Path.Combine((ConfigurationManager.AppSettings["UploadPath"]), "TEST.jpg");
            var byteArray = File.ReadAllBytes(path);
            var ms = new MemoryStream(byteArray);
            var image = Image.FromStream(ms);
            var pathToDeleteFile = Path.Combine(ConfigurationManager.AppSettings["UploadPath"], "TestFile.jpg");

            image.Save(pathToDeleteFile, ImageFormat.Jpeg);

            var result = _handler.DeleteFile(pathToDeleteFile);

            Assert.IsTrue(result, "File was deleted!");

            if (File.Exists(pathToDeleteFile))
            {
                File.Delete(pathToDeleteFile);
            }
        }
        [TestMethod]
        public void TestReceivePhoto()
        {
            HttpPostedFileBase httpPostedFile = Mock.Of<HttpPostedFileBase>();
            var mock = Mock.Get(httpPostedFile);

            var testPhoto = new Photo();

            testPhoto.ActualSizeName = "TestFileActualSizeName.jpg";
            testPhoto.MediumSizeName = "TestFileMediumSizeName.jpg";
            testPhoto.SmallSizeName = "TestFileSmallSizeName.jpg";

            var path = (ConfigurationManager.AppSettings["UploadPath"]);
            var pathToFile = Path.Combine(path, "TEST.jpg");
            var byteArray = File.ReadAllBytes(pathToFile);
            var ms = new MemoryStream(byteArray);

            var image =  Image.FromStream(ms);

            image.Save(path+testPhoto.ActualSizeName, ImageFormat.Jpeg);
            image.Save(path+testPhoto.MediumSizeName, ImageFormat.Jpeg);
            image.Save(path+testPhoto.SmallSizeName, ImageFormat.Jpeg);

            var result = _handler.ReceivePhoto(httpPostedFile, testPhoto, path);

            Assert.IsTrue(result);
        }

    }
    class MemoryFile : HttpPostedFileBase
    {
        Stream stream;
        string contentType;
        string fileName;

        public MemoryFile(Stream stream, string contentType, string fileName)
        {
            this.stream = stream;
            this.contentType = contentType;
            this.fileName = fileName;
        }

        public override int ContentLength
        {
            get { return (int)stream.Length; }
        }

        public override string ContentType
        {
            get { return contentType; }
        }

        public override string FileName
        {
            get { return fileName; }
        }

        public override Stream InputStream
        {
            get { return stream; }
        }

        public override void SaveAs(string filename)
        {
            using (var file = File.Open(filename, FileMode.CreateNew))
                stream.CopyTo(file);
        }
    }
}
