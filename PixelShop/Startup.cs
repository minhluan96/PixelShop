using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PixelShop.Startup))]
namespace PixelShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
