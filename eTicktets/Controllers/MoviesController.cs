using eTicktets.Data;
using eTicktets.Data.Services;
using eTicktets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eTicktets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService service;

        // private readonly AppDbContext _context;
        public MoviesController(IMoviesService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await service.GetAllAsync(n => n.Cinema);
            return View(data);
        }
        
        public async Task<IActionResult> Filter(string searchString)
        {
            var data = await service.GetAllAsync(n => n.Cinema);

            if(!string.IsNullOrEmpty(searchString))
            {
                var filterResult = data.Where(m => m.Name.Contains(searchString) || 
                m.Description.Contains(searchString)).ToList();

                return View("Index", filterResult);
            } 
            return View("Index", data);
        }
        
        // GET : Movies/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var movieDetials = await service.GetMovieByIdAsync(id);

            return View(movieDetials);
        }

        // GET : Movies/Create
        public async Task<IActionResult> Create()
        {
            var movieDropdownsData = await service.GetNewMovieDropdownValues();

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas,"Id","Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers,"Id","FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors,"Id","FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await service.GetNewMovieDropdownValues();

                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

                return View(movie);
            }

            await service.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }

        // GET : Movies/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var movieDetails = await service.GetMovieByIdAsync(id);
            if (movieDetails == null) return View("NotFound");

            var response = new NewMovieVM()
            {
                Id = id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,
                StartDate = movieDetails.StartDate,
                EndDate = movieDetails.EndDate,
                ImageUrl = movieDetails.ImageUrl,
                MovieCategory = movieDetails.MovieCategory,
                CinemaId = movieDetails.CinemaId,
                ProducerId = movieDetails.ProducerId,
                ActorIds = movieDetails.Actors_Movies.Select(a => a.ActorId).ToList()
            };

            var movieDropdownsData = await service.GetNewMovieDropdownValues();

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMovieVM movie)
        {
            if (id != movie.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await service.GetNewMovieDropdownValues();

                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

                return View(movie);
            }

            await service.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }
    }
}
