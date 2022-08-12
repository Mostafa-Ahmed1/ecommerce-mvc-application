using eTicktets.Data;
using eTicktets.Data.Services;
using eTicktets.Models;
using Microsoft.AspNetCore.Mvc;

namespace eTicktets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService service;
        public ActorsController(IActorsService service)
        {
            this.service = service;
        }


        public async Task<IActionResult> Index()
        {
            var data = await service.GetAllAsync();
            return View(data);
        }
        // Get: Actors/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName, ProfilePicture, Bio")]Actor actor)
        {
            // to check validation u created in class
            //if (!ModelState.IsValid) return View();
            if (ModelState.IsValid)
            {
                service.AddAsync(actor);
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        // Get: Actors/Details/Id
        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await service.GetByIdAsync(id);

            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

        // Get: Actors/Update
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await service.GetByIdAsync(id);

            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }
        // POST
        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id,FullName, ProfilePicture, Bio")] Actor actor)
        {
            if (ModelState.IsValid)
            {
                service.UpdateAsync(id,actor);
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        // Get: Actors/Delete/id
        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await service.GetByIdAsync(id);

            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }
        // POST
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorDetails = await service.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");

            await service.DeleteAsync(id);
           
            return RedirectToAction(nameof(Index));
        }
    } 
}
