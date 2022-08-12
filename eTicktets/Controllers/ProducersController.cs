using eTicktets.Data;
using eTicktets.Data.Services;
using eTicktets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTicktets.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducersService service;

        public ProducersController(IProducersService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            var allProducers = await service.GetAllAsync();

            return View(allProducers);
        }

        //GET : Produers/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var producerDetails = await service.GetByIdAsync(id);

            if (producerDetails == null) 
                return View("NotFound");
                return View(producerDetails);
        }

        // GET : Producers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName, ProfilePicture, Bio")] Producer producer)
        {
            // to check validation u created in class

            if (ModelState.IsValid)
            {
                service.AddAsync(producer);
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }

        // GET : Producers/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var producerDetails = await service.GetByIdAsync(id);

            if (producerDetails == null)
                return View("NotFound");
            return View(producerDetails);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id,FullName, ProfilePicture, Bio")] Producer producer)
        {
            // to check validation u created in class

            if (!ModelState.IsValid) 
                return View(producer);

            if (id == producer.Id)
            {
                service.UpdateAsync(id, producer);
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }

        // GET : Producers/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var producerDetails = await service.GetByIdAsync(id);

            if (producerDetails == null)
                return View("NotFound");
            return View(producerDetails);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producerDetails = await service.GetByIdAsync(id);

            if (producerDetails == null)
                return View("NotFound");

            await service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
