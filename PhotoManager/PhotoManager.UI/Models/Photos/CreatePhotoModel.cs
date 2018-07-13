using System.ComponentModel.DataAnnotations;
using System.Web;

namespace PhotoManager.UI.Models.Photos
{
    public class CreatePhotoModel : BasePhotoModel
    {
        [Required]
        public HttpPostedFileBase File { get; set; }

    }
}