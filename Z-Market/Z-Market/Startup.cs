using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Z_Market.Startup))]
namespace Z_Market
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
