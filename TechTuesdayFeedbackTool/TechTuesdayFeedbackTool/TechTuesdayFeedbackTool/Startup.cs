using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TechTuesdayFeedbackTool.Startup))]
namespace TechTuesdayFeedbackTool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
