using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using PhotoAlbumBLL.Interfaces;
using PhotoAlbumBLL.Services;
using PhotoAlbumDAL;
using PhotoAlbumDAL.Models;

namespace PhotoAlbum.Controllers
{
    public class UsersController : Controller
    {
        private readonly IService<User> _service;

        public UsersController(IService<User> service)
        {
            _service = service;
        }

        // GET: Users
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index()
        {
            var d = User.IsInRole("Admin");
            if (!User.Identity.IsAuthenticated)
            {
                // var df= User.Identity.
                return View(await _service.GetAllAsync());
            }

            var f = await _service.GetAllAsync();

            return View(f.Where(x => x.FirstName + x.LastName == User.Identity.Name));
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await _service.GetById(id));
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        public async Task<ActionResult> Create(User user)
        {
            try
            {
                await _service.CreateAsync(user);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Login()
        {
            var d = User.IsInRole("Admin");
            if (!User.Identity.IsAuthenticated)
            {
                // var df= User.Identity.
                return View();
            }

            return Redirect("Index");
        }

        [HttpPost]
        public ActionResult Login(LoginInfo loginInfo)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("Login");
            }

            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            var user = manager.Find(loginInfo.Username, loginInfo.Password);
            var d = manager.GetRoles(user.Id);
            ClaimsIdentity userIdentity = manager.CreateIdentity(user,
                DefaultAuthenticationTypes.ApplicationCookie);
            authenticationManager.SignIn(new AuthenticationProperties() { }, userIdentity);

            return Redirect("Index");
        }

        [HttpGet]
        public ActionResult SignOut()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();

            return Redirect("Index");
        }
    }
}