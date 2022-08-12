using eTicktets.Data;
using eTicktets.Data.Services;
using eTicktets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTicktets.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemasService service;

        public CinemasController(ICinemasService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await service.GetAllAsync();

            return View(data);
        }

        // GET : Cinemas/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, Logo, Description")]Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema);

            await service.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }

        // GET : Cinemas/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var cinemaDetails = await service.GetByIdAsync(id);

            if (cinemaDetails == null) 
                return View("NotFound");

            return View(cinemaDetails);
        }

        // GET : Cinemas/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var cinemaDetails = await service.GetByIdAsync(id);

            if (cinemaDetails == null)
                return View("NotFound");

            return View(cinemaDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id, Name, Logo, Description")] Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema);

            await service.UpdateAsync(id, cinema);
            return RedirectToAction(nameof(Index));
        }

        // GET : Cinemas/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var cinemaDetails = await service.GetByIdAsync(id);

            if (cinemaDetails == null)
                return View("NotFound");

            return View(cinemaDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cinemaDetails = await service.GetByIdAsync(id);

            if (cinemaDetails == null)
                return View("NotFound");
            
            await service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
