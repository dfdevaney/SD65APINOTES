using GeneralStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Mvc;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;

namespace GeneralStoreAPI.Controllers
{
    public class ProductController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // Post:

        [HttpPost]
        public async Task<IHttpActionResult> Post(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        // Get All:

        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Product> listOfProducts = await _context.Products.ToListAsync();
            return Ok(listOfProducts);
        }

        // Get By ID:

        [HttpGet]
        public async Task<IHttpActionResult> GetByID([FromUri]int id)
        {
            Product foundProduct = await _context.Products.FindAsync(id);
            if(foundProduct == null)
            {
                return NotFound();
            }
            return Ok(foundProduct);
        }

        // Put:

        [HttpPut]
        public async Task<IHttpActionResult> Put([FromUri]int id, [FromBody]Product model)
        {
            if(ModelState.IsValid)
            {
                if(id != model.ID)
                {
                    return BadRequest("Product ID Mismatch ...");
                }
                _context.Entry(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        // Delete:

        [HttpDelete]
        public async Task<IHttpActionResult> Delete([FromUri]int id)
        {
            var foundProduct = await _context.Products.FindAsync(id);
            if(foundProduct == null)
            {
                return NotFound();
            }
            _context.Products.Remove(foundProduct);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
