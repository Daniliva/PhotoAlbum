using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PhotoAlbumBLL.Interfaces;
using PhotoAlbumBLL.Services;

namespace PhotoAlbum.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IPhotoService _photoService;
        private IUserService _userService;

        public PhotoController(IPhotoService photoService, IUserService userService)
        {
            _photoService = photoService;
            _userService = userService;
        }

        // GET: Photo
        public async Task<ActionResult> Index()
        {
            var users = await _userService.GetAllAsync();
            var user = users.FirstOrDefault(x => x.Email == User.Identity.Name);
            if (user != null) return View(await _photoService.GetAllByUserLoginAsync(user.Login));
            return View();
            // return View(await _photoService.GetAllAsync());
        }

        // GET: Photo/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await _photoService.GetById(id));
        }
    }
}