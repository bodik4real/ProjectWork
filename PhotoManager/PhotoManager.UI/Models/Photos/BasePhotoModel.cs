using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoManager.UI.Models.Photos
{
    public class BasePhotoModel
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Date:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public string Description { get; set; }
        [Display(Name = "Shutter Speed")]
        public string ShutterSpeed { get; set; }
        public string Diaphragm { get; set; }
        public bool Flash { get; set; }

    }
}