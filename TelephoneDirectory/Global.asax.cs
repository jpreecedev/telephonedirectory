namespace TelephoneDirectory
{
    using System.Data.Entity;
    using System.Web;
    using System.Web.Http;

    using TelephoneDirectory.Models;

    public class MvcApplication : HttpApplication
    {
        #region Methods

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Database.SetInitializer(new Initializer());
        }

        #endregion
    }
}