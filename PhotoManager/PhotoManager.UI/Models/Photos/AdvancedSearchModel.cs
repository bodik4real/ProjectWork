using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoManager.UI.Models.Photos
{
    public class AdvancedSearchModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name="Shutter Speed")]
        public string ShutterSpeed { get; set; }
        public string Diaphragm { get; set; }
        public bool Flash { get; set; } = false;
    }
}