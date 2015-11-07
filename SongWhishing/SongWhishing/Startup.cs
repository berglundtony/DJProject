using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SongWishing.Startup))]
namespace SongWishing
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
