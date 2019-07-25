using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MonsterLoots.WebMVC.Startup))]
namespace MonsterLoots.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
