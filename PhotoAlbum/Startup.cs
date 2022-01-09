using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;

using Microsoft.Owin.Security.Cookies;
using Owin;
using PhotoAlbumDAL.Models;

[assembly: OwinStartup(typeof(PhotoAlbum.Startup))]

namespace PhotoAlbum
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Дополнительные сведения о настройке приложения см. на странице https://go.microsoft.com/fwlink/?LinkID=316888 app.UseCookieAuthentication(new CookieAuthenticationOptions
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Users/Login"),
                LogoutPath = new PathString("/Users/SignOut")
            });
        }
    }
}