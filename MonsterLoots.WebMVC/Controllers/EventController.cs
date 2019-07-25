using Microsoft.AspNet.Identity;
using MonsterLoots.Models.Event;
using MonsterLoots.Services;
using MonsterLoots.WebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MonsterLoots.WebMVC.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Event
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var monsters = _db.Monsters.Where(t => t.OwnerId == userId).ToList();
            ViewBag.MonsterId = new SelectList(monsters, "MonsterId", "MonsterName");

            var service = GetEventService();
            var historyList = service.GetHistory();
            ViewBag.historyList = historyList;

            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult GetSelectedMonsters(EventModel model) // Taking in selected monster
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var monsters = _db.Monsters.Where(t => t.OwnerId == userId).ToList();
            ViewBag.MonsterId = new SelectList(monsters, "MonsterId", "MonsterName");

            if (!ModelState.IsValid) // If what they input is valid (selecting a monster)
            {
                return View(model); // Error
            }
            var service = GetEventService(); // Something something validate user

            model.MonsterName = service.GetMonsterById(model.MonsterId).MonsterName;

            EventModel newModel = service.RandomLoot(model);

            newModel.MonsterName = model.MonsterName;

            if (service.AddHistory(newModel))
            {
                if (newModel != null)
                {
                    TempData["SaveResult"] = $"{newModel.LootName}";
                }
            }
            var historyList = service.GetHistory();

            ViewBag.historyList = historyList;

            return View();
        }

        public EventService GetEventService() // Get User by Guid
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new EventService(userId);
            return service;
        }
    }
}