using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GreetUP.Startup))]
namespace GreetUP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
