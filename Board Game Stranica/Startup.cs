using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Board_Game_Stranica.Startup))]
namespace Board_Game_Stranica
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
