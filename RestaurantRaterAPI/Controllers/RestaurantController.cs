using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRaterAPI.Controllers
{
    public class RestaurantController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();

        // Post:

        [HttpPost]
        public async Task<IHttpActionResult> PostRestaurant(Restaurant model)
        {
            if(ModelState.IsValid)
            {
                _context.Restaurants.Add(model);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        // Get All:

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            List<Restaurant> listOfRestaurants = await _context.Restaurants.ToListAsync();
            return Ok(listOfRestaurants);
        }

        // Get By ID:

        [HttpGet]
        public async Task<IHttpActionResult> GetByID(int id)
        {
            Restaurant oneRestaurant = await _context.Restaurants.FindAsync(id);
            if(oneRestaurant != null)
            {
                return Ok(oneRestaurant);
            }
            return NotFound();
        }

        // Put:

        [HttpPut]
        public async Task<IHttpActionResult> UpdateRestaurant([FromUri] int id, [FromBody] Restaurant model)
        {
            if (ModelState.IsValid)
            {
                var foundRestaurant = await _context.Restaurants.FindAsync(id);
                if (foundRestaurant != null)
                {
                    foundRestaurant.Name = model.Name;
                    foundRestaurant.Address = model.Address;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        // Delete:

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRestaurantByID(int id)
        {
            var foundRestaurant = await _context.Restaurants.FindAsync(id);
            if(foundRestaurant == null)
            {
                return NotFound();
            }
            _context.Restaurants.Remove(foundRestaurant);
            if(await _context.SaveChangesAsync() == 1)
            {
                return Ok();
            }
            return InternalServerError();
        }
    }
}
