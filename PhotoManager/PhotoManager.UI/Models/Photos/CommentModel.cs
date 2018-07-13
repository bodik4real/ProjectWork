﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoManager.UI.Models.Photos
{
    public class CommentModel
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "PhotoId is required!")]
        public int photoId { get; set; }
        [Required]
        public string Text { get; set; }
    }
}