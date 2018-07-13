using PhotoManager.DAL.Entities;
using System.Collections.Generic;

namespace PhotoManager.UI.Models.Photos
{
    public class AdvancedSearchAndPhotos
    {
        public AdvancedSearchModel AdvancedSearch { get; set; }
        public IEnumerable<Photo> Photos { get; set; }
    }
}