using System.Web.Mvc;
using System.Web.Routing;

namespace PhotoManager.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "GetPhotosByAlbumName",
                url: "Albums/GetPhotosByAlbumName/{albumName}",
                defaults: new { controller = "Albums", action = "GetPhotosByAlbumName", albumName = UrlParameter.Optional });

            routes.MapRoute(
                name: "AlbumById",
                url: "Albums/Delete/{albumId}",
                defaults: new { controller = "Albums", action = "Delete", albumId = UrlParameter.Optional });

            routes.MapRoute(
               name: "PhotoById",
               url: "Photos/Delete/{photoId}",
               defaults: new { controller = "Photos", action = "Delete", photoId = UrlParameter.Optional });

            routes.MapRoute(
             name: "Detach",
             url: "Albums/Detach/{photoId}/{albumId}",
             defaults: new { controller = "Albums", action = "Detach", photoId = UrlParameter.Optional, albumId = UrlParameter.Optional });

            routes.MapRoute(
             name: "SetAsTitle",
             url: "Albums/SetAsTitle/{albumTitle}/{albumId}",
             defaults: new { controller = "Albums", action = "SetAsTitle", albumTitle = UrlParameter.Optional, albumId = UrlParameter.Optional });
            
            routes.MapRoute(
                name: "UpdateAlbum",
                url: "Albums/Update/{albumId}",
                defaults: new { controller = "Albums", action = "Update", albumId = UrlParameter.Optional });

            routes.MapRoute(
               name: "UpdatePhoto",
               url: "Photos/Update/{photoId}",
               defaults: new { controller = "Photos", action = "Update", photoId = UrlParameter.Optional });
            
            routes.MapRoute(
               name: "GetPhotosByAlbumId",
               url: "Albums/GetPhotosByAlbumId/{albumId}",
               defaults: new { controller = "Albums", action = "GetPhotosByAlbumId", albumId = UrlParameter.Optional });

            routes.MapRoute(
                name: "GetPhotosToAttach",
                url: "Photos/GetPhotosToAttach/{albumId}",
                defaults: new { controller = "Photos", action = "GetPhotosToAttach", albumId = UrlParameter.Optional });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Albums", action = "GetAll", id = UrlParameter.Optional }
            );
        }
    }
}
