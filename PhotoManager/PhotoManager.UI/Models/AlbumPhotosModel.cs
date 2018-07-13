using System.ComponentModel.DataAnnotations;

namespace PhotoManager.UI.Models
{
    public class AlbumPhotosModel
    {
        [Range(1, int.MaxValue)]
        [Required(ErrorMessage = "Album Id is required")]
        public int AlbumId { get; set; }

        [Range(1, int.MaxValue)]
        [Required(ErrorMessage = "Photo Id is required")]
        public int PhotoId { get; set; }

        public bool IsChecked { get; set; } = false;

        public string MediumSizeName { get; set; }
    }
}