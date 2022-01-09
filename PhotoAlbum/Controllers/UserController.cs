using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using PhotoAlbumBLL.DTO;
using PhotoAlbumBLL.Interfaces;
using PhotoAlbumBLL.Services;
using PhotoAlbumDAL.Models;

namespace PhotoAlbum.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: User

        public async Task<ActionResult> Index()
        {
            return View(await _userService.GetAllAsync());
        }

        // GET: User/Details/5
        public async Task<ActionResult> DetailsInfo(int userId)
        {
            return View(await _userService.GetById(userId));
        }

        public async Task<ActionResult> Details()
        {
            var users = await _userService.GetAllAsync();

            return View(users.FirstOrDefault(x => x.Email == User.Identity.Name));
        }

        public ActionResult Login()
        {
            //   var d = User.IsInRole("Admin");
            if (!User.Identity.IsAuthenticated)
            {
                // var df= User.Identity.
                return View();
            }

            return Redirect("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginInfo loginInfo)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("Login");
            }
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            ClaimsIdentity userIdentity = await _userService.Login(loginInfo, DefaultAuthenticationTypes.ApplicationCookie);
            authenticationManager.SignIn(new AuthenticationProperties() { }, userIdentity);

            return Redirect("Index");
        }

        [HttpGet]
        public ActionResult SignOut()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();

            return Redirect("Login");
        }

        // GET: User/Create
        public ActionResult Registration()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public async Task<ActionResult> Registration(UserCreate userCreate)
        {
            try
            {
                await _userService.CreateAsync(userCreate);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public async Task<ActionResult> Edit()
        {
            var d = await _userService.GetAllAsync();
            var user = d.FirstOrDefault(x => x.Email == User.Identity.Name);
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(User user)
        {
            try
            {
                await _userService.ChangeAsync(user);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> ChangePassword()
        {
            var d = await _userService.GetAllAsync();
            var user = d.FirstOrDefault(x => x.Email == User.Identity.Name);

            if (user != null) return View(new UserUpdate() { LoginNew = user.Login });
            return RedirectToAction("Index");
            //ChangePassword
        }

        // POST: User/Edit/5
        [HttpPost]
        public async Task<ActionResult> ChangePassword(UserUpdate userCreate)
        {
            try
            {
                var d = await _userService.GetAllAsync();
                var user = d.FirstOrDefault(x => x.Email == User.Identity.Name);

                await _userService.ChangeLoginAndPasswordAsync(user, userCreate);
                return RedirectToAction("Edit");
            }
            catch
            {
                return View();
            }
        }
    }
}