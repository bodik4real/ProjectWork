using AutoMapper;
using PhotoManager.DAL.Entities;
using PhotoManager.UI.Models.Albums;
using PhotoManager.UI.Models.Photos;

namespace PhotoManager.UI.App_Start
{
    public static class AutomapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CreatePhotoModel, Photo>();   //Config for Photo
                cfg.CreateMap<UpdatePhotoModel, Photo>();
                cfg.CreateMap<Photo, UpdatePhotoModel>();

                cfg.CreateMap<CreateAlbumModel, Album>();   //Config for Album
                cfg.CreateMap<UpdateAlbumModel, Album>();
                cfg.CreateMap<Album, UpdateAlbumModel>();
            });
        }
    }
}