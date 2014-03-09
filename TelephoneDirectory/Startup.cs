using Microsoft.Owin;

[assembly: OwinStartup(typeof(TelephoneDirectory.Startup))]

namespace TelephoneDirectory
{
    using Owin;

    public class Startup
    {
        #region Public Methods and Operators

        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }

        #endregion
    }
}