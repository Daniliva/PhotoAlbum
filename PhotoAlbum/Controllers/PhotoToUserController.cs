using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PhotoAlbumBLL.DTO;
using PhotoAlbumBLL.Interfaces;
using PhotoAlbumBLL.Services;
using PhotoAlbumDAL.Models;

namespace PhotoAlbum.Controllers
{
    public class PhotoToUserController : Controller
    {
        private readonly IPhotoService _photoService;
        private IUserService _userService;
        private PhotoUserService _photoUserService;

        public PhotoToUserController(IPhotoService photoService, IUserService userService, PhotoUserService photoUserService)
        {
            _photoService = photoService;
            _userService = userService;
            _photoUserService = photoUserService;
        }

        // GET: PhotoToUser
        public async Task<ActionResult> Index()
        {
            return View(await _photoService.GetAllByUserLoginAsync(User.Identity.Name));
        }

        // GET: PhotoToUser/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await _photoService.GetById(id));
        }

        // GET: PhotoToUser/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhotoToUser/Create
        [HttpPost]
        public async Task<ActionResult> Create(PhotoCreate collection)
        {
            try
            {
                await _photoService.CreateAsync(collection, User.Identity.Name);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PhotoToUser/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var collection = await _photoService.GetAllByUserLoginAsync(User.Identity.Name);

            return View(collection.FirstOrDefault(x => x.Id == id));
        }

        // POST: PhotoToUser/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PhotoToUser/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PhotoToUser/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}