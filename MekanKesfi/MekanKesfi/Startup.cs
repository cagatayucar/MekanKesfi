using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MekanKesfi.Startup))]
namespace MekanKesfi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
