using Microsoft.Owin;
using Owin;
using SimpleInjector;
using SimpleInjector.Lifestyles;

[assembly: OwinStartup(typeof(PhotoManager.UI.Startup))]
namespace PhotoManager.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}