using System.Reflection;
using System.Web.Mvc;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.Repositories;
using PhotoManager.Services;
using PhotoManager.Services.PhotosHandler;
using PhotoManager.DAL;
using PhotoManager.DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PhotoManager.DAL.Identity;
using System.Web;
using Microsoft.Owin;
using System.Web.Http;
using SimpleInjector.Integration.WebApi;

namespace PhotoManager.UI
{
    public static class SimpleInjectorConfig
    {
        public static void Initialize(Container container)
        {
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());          //MVC Controllers
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);     //Web API Controllers

            container.Register<PhotoManagerDbContext>(Lifestyle.Scoped);
            container.Register<DAL.Identity.IdentityDbContext>(Lifestyle.Scoped);

            container.Register<IAlbumsRepository, AlbumsRepository>();                  //Repositories
            container.Register<IPhotosRepository, PhotosRepository>();                  
            container.Register<ILikeRepository, LikeRepository>();
            container.Register<ICommentRepository, CommentRepository>();

            container.Register<IPhotoService, PhotosService>();                         //Services
            container.Register<IAlbumsService, AlbumsService>();
            container.Register<ILikeService, LikeService>();
            container.Register<ICommentService, CommentService>();
            
            container.Register<IPhotoHandler, PhotoHandler>();

            container.Register<IUserStore<User>>(() => new UserStore<User>(container.GetInstance<DAL.Identity.IdentityDbContext>()), Lifestyle.Scoped);


            container.Register<ApplicationSignInManager>(Lifestyle.Scoped);
            container.Register<ApplicationUserManager>(Lifestyle.Scoped);

            container.Register(() => container.IsVerifying ? new OwinContext().Authentication : HttpContext.Current.GetOwinContext().Authentication, Lifestyle.Scoped);

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));


            GlobalConfiguration.Configuration.DependencyResolver =
            new SimpleInjectorWebApiDependencyResolver(container);

        }
    }
}
