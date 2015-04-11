using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RazorFromApiSpike.Web.Startup))]
namespace RazorFromApiSpike.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
