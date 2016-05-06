using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewAspIdentity.Startup))]
namespace NewAspIdentity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
