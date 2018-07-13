using System;
using System.ComponentModel.DataAnnotations;


namespace PhotoManager.UI.Models.Albums
{
    public class BaseAlbumModel
    {
        [Required(ErrorMessage = "Name is required!")]
        
        public string Name { get; set; }

        public string UserId { get; set; }

        [Required]
        [Display(Name = "Date:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public string Description { get; set; }
    }
}