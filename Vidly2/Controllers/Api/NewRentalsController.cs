using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly2.Dtos;
using Vidly2.Models;

namespace Vidly2.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context = null;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto rentalDto)
        {
            var customer = _context.Customers.Single(c => c.Id == rentalDto.CustomerId);

            var movies = _context.Movies.Where(m => rentalDto.MovieIds.Contains(m.Id));

            foreach(var movie in movies)
            {
                movie.NumberAvailable--;

                var rental = new Rental
                {
                    DateRented = DateTime.Now,
                    Movie = movie,
                    Customer = customer,
                };

                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();

            return Ok();
        }


    }
}
