using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly2.Models;
using Vidly2.ViewModels;

namespace Vidly2.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };

            var customers = new List<Customer>
            {
                new Customer{ Name = "Customer 1"},
                new Customer{ Name = "Customer 2"},
                new Customer{ Name = "Customer 1"},
                new Customer{ Name = "Customer 1"},
                new Customer{ Name = "Customer 1"},
                new Customer{ Name = "Customer 1"},
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };



            return View(viewModel);
        }

        // movies/released/year/month
        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

    
        public ActionResult Index(int? pageIndex, string sortBy)
        {
            var movies = _context.Movies.ToList();
            var genres = new List<Genre>();


            foreach (var movie in movies)
            {
                genres.Add(_context.Genres.Where(g => g.Id == movie.GenreId).FirstOrDefault());
            }

            var moviesWithGenres = new MoviesWithGenres
            {
                Movies = movies,
                Genres = genres
            };

            return View(movies);


        }

        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                var moviesWithGenres = _context.Movies.Include("Genre").ToList();
                var selectedMovie = moviesWithGenres.Where(movie => movie.Id == id).FirstOrDefault();

                return View(selectedMovie);
            }
            return Content("nothing");
        }

        public ActionResult New()
        {
            var genres = _context.Genres.ToList();

            var viewModel = new MovieFormViewModel
            {
                Genres = genres
            };


            return View("MovieForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = _context.Genres.ToList()
            };

            return View("Movieform", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm", viewModel);
            }


            if(movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                movie.Genre = _context.Genres.Where(x => x.Id == movie.GenreId).First();
                _context.Movies.Add(movie);
            }
            else
            {
                var movieToUpdate = _context.Movies.Where(m => m.Id == movie.Id).First();
                movieToUpdate.Name = movie.Name;
                movieToUpdate.NumberInStock = movie.NumberInStock;
                movieToUpdate.ReleaseDate = movie.ReleaseDate;
                movieToUpdate.GenreId = movie.GenreId;
                movieToUpdate.Genre = _context.Genres.Where(genre => genre.Id == movie.GenreId).First();
            }
            
            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }
    }
}