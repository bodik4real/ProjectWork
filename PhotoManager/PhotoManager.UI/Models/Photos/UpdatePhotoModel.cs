using System.ComponentModel.DataAnnotations;
using System.Web;

namespace PhotoManager.UI.Models.Photos
{
    public class UpdatePhotoModel : BasePhotoModel
    {

        [Range(1, int.MaxValue)]
        [Required(ErrorMessage = "Photo Id is required")]
        public int Id { get; set; }
        public HttpPostedFileBase File { get; set; }

        public string ActualSizeName { get; set; }
        public string MediumSizeName { get; set; }
        public string SmallSizeName { get; set; }

    }
}