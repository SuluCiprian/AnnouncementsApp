using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Announcements.Services;
using AnnouncementsApp.Domain;
using AnnouncementsApp.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Announcements.Controllers
{
    public class AnnouncementsController : Controller
    {
        private readonly IUnitOfWork _uitOfWork;
        private readonly IUserService _userService;

        public AnnouncementsController(IUnitOfWork unitOfWork, IUserService userService)
        {
            _uitOfWork = unitOfWork;
            _userService = userService;
        }

        [Authorize]
        public IActionResult Index()
        {
            var announcements = _uitOfWork.Announcements.GetAll();
            return View(announcements);
        }

        [Authorize]
        public IActionResult Create()
        {
            Announcement vm = new Announcement();

            return View(vm);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                announcement.Owner = _userService.GetSignedInUser();
                _uitOfWork.Announcements.Insert(announcement);
                _uitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(announcement);
        }
        
        [Authorize]
        public IActionResult Delete(int id)
        {
            var announcement = _uitOfWork.Announcements.GetById(id);
            if (announcement == null)
            {
                return NotFound();
            }

            return View(announcement);
        }
        
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var announcement = _uitOfWork.Announcements.GetById(id);
            bool admin = await _userService.IsAdmin();
            
            
            if (admin)
            {
                _uitOfWork.Announcements.Delete(announcement);
                _uitOfWork.Complete();
            } else
            {
                var signedInUser = _userService.GetSignedInUser();
                if (announcement.Owner == signedInUser)
                {
                    _uitOfWork.Announcements.Delete(announcement);
                    _uitOfWork.Complete();
                }
            }            

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpGet]
        [Route("Announcements/Edit/{id}", Name = "EditAnnouncements")]
        public IActionResult Edit(int id)
        {
            var announcement = _uitOfWork.Announcements.GetById(id);
            var signedInUser = _userService.GetSignedInUser();
            if (announcement.Owner == signedInUser)
            {
                return View();
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Announcements/Edit")]
        public IActionResult Edit(Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(announcement);
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var vm = new Announcement();

                vm = _uitOfWork.Announcements.GetById(id);

                return View(vm);
            }
        }

    }
}