using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ILoveYou.Startup))]
namespace ILoveYou
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
