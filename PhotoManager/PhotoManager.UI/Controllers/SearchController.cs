using PhotoManager.DAL.Entities;
using PhotoManager.Services;
using System.Web.Mvc;
using PhotoManager.UI.Models.Photos;

namespace PhotoManager.UI.Controllers
{
    public class SearchController : Controller
    {
        private IPhotoService _service;
        public SearchController(IPhotoService service)
        {
            _service = service;           
        }

        [HttpGet]
        public ActionResult AdvancedSearch()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdvancedSearch(AdvancedSearchAndPhotos model)
        {
            var photoForSearch = new Photo();

            photoForSearch.Name = model.AdvancedSearch.Name;
            photoForSearch.Description = model.AdvancedSearch.Description;
            photoForSearch.ShutterSpeed = model.AdvancedSearch.ShutterSpeed;
            photoForSearch.Flash = model.AdvancedSearch.Flash;
            photoForSearch.Diaphragm = model.AdvancedSearch.Diaphragm;
            
            var searchedItem = new AdvancedSearchAndPhotos();
            searchedItem.Photos = _service.AdvancedSearch(photoForSearch);

            return View("AdvancedSearchResults", searchedItem);
        }

        [HttpPost]
        public ActionResult Search(string searchingString)
        {
            var photos = _service.Search(searchingString);

            return View(photos);
        }
    }
}