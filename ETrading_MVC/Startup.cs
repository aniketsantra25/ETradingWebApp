using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ETrading_MVC.Startup))]
namespace ETrading_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
