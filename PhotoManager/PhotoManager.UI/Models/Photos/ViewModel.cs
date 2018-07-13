using PhotoManager.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoManager.UI.Models.Photos
{
    public class ViewModel
    {
        public AdvancedSearchModel AdvancedSearch { get; set; }
        public IEnumerable<Photo> Photos { get; set; }
    }
}