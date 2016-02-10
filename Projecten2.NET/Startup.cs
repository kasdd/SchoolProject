using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Projecten2.NET.Startup))]
namespace Projecten2.NET
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
