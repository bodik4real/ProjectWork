using PhotoManager.DAL.Entities;
using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Hosting;

namespace PhotoManager.Services.PhotosHandler
{
    public class PhotoHandler : IPhotoHandler
    {
        public bool ReceivePhoto(HttpPostedFileBase file, Photo photo)
        {
            var mediumSizeRaw = ConfigurationManager.AppSettings["MediumSizeName"].ToString(); 
            var smallSizeRaw = ConfigurationManager.AppSettings["SmallSizeName"].ToString();
            int mediumSize;
            int smallSizeName;

            if (!(int.TryParse(mediumSizeRaw, out mediumSize) && int.TryParse(smallSizeRaw ,out smallSizeName)))
            {
                return false;
            }

            CutAndSave(file, photo.ActualSizeName);
            CutAndSave(file, photo.MediumSizeName, mediumSize);
            CutAndSave(file, photo.SmallSizeName, smallSizeName); 
            return true;
        }
        
        public void DeleteFile(string fileName)
        {
            var path = Path.Combine(HostingEnvironment.MapPath(ConfigurationManager.AppSettings["UploadPath"]), fileName);
            File.Delete(path);
        }
        private void CutAndSave(HttpPostedFileBase file, string fileName, int cutPixels = 0)
        {
            Bitmap croppedImage;
            var path = Path.Combine(HostingEnvironment.MapPath(ConfigurationManager.AppSettings["UploadPath"]), fileName);
            file.SaveAs(path);

            if (cutPixels != 0)
            {
                using (var src = new Bitmap(path))
                {
                    var cropRect = new Rectangle((src.Width / 2) - cutPixels, (src.Height / 2) - cutPixels, (src.Width / 2) + cutPixels, (src.Height / 2) + cutPixels);
                    croppedImage = src.Clone(cropRect, src.PixelFormat);
                }
                croppedImage.Save(path);
                croppedImage.Dispose();
            }
        }

    }
}





