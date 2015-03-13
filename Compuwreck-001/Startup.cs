using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Compuwreck_001.Startup))]
namespace Compuwreck_001
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
