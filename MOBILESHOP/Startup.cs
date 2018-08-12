using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MOBILESHOP.Startup))]
namespace MOBILESHOP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
