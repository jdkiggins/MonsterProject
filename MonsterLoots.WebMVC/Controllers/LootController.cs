using Microsoft.AspNet.Identity;
using MonsterLoots.Models.Loot;
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
    public class LootController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Loot
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new LootService(userId);
            var model = service.GetLoot();

            return View(model);
        }
        public ActionResult Create() // ViewBag to bring the information to the drop down list to access the Monsters
        {
            var monsters = _db.Monsters.ToList().Where(t => t.OwnerId == Guid.Parse(User.Identity.GetUserId()));
            ViewBag.MonsterId = new SelectList(monsters, "MonsterId", "MonsterName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LootCreate model) //POST: Takes in written the information as 'model'
        {
            if (!ModelState.IsValid) return View(model); // Checks if the input requirements have been met. If not, return what the user wrote in the box (error)

            var service = CreateLootService(); // Service is set to the user's information (euid)

            if (service.CreateLoot(model)) // Sends the information to the service (CreateLoot)
            {
                TempData["SaveResult"] = $"{model.LootName} has been created."; // If it was sent, send a confirmation message
                return RedirectToAction("Index"); // Returns to the index page (list view)
            };

            ModelState.AddModelError("", $"{model.LootName} could not be created.");

            return View(model);
        }

        private LootService CreateLootService() // Verification to set the new information to the user's list
        {
            var userId = Guid.Parse(User.Identity.GetUserId()); // Gets the user's Id
            var service = new LootService(userId); // Sets service to the logged in user as the Guid
            return service; // Returns the user's id
        }
        public ActionResult Details(int id)
        {
            var svc = CreateLootService();
            var model = svc.GetLootById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var monsters = _db.Monsters.ToList().Where(t => t.OwnerId == Guid.Parse(User.Identity.GetUserId())); //Getting logged in user's list
            ViewBag.MonsterId = new SelectList(monsters, "MonsterId", "MonsterName");

            var service = CreateLootService();
            var detail = service.GetLootById(id);
            var model =
                new LootEdit
                {
                    LootId = detail.LootId,
                    LootName = detail.LootName,
                    LootDesc = detail.LootDesc,
                    MonsterId = detail.MonsterId
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LootEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.LootId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateLootService();

            if (service.UpdateLoot(model))
            {
                TempData["SaveResult"] = ($"{model.LootName} was updated.");
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", ($"{model.LootName} could not be updated."));
            return View(model);
        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateLootService();
            var model = svc.GetLootById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTheMonster(int id)
        {
            var service = CreateLootService();

            var model = service.GetLootById(id);

            service.DeleteLoot(id);

            TempData["SaveResult"] = $"{model.LootName} has been deleted";
            return RedirectToAction("Index");
        }
    }
}