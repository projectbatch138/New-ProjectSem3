using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AirlinesReservationSystem.Startup))]
namespace AirlinesReservationSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
