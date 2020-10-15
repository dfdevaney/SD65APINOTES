using GeneralStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GeneralStoreAPI.Controllers
{
    public class TransactionController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // Post:

        [HttpPost]
        public async Task<IHttpActionResult> Post(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.DateOfTransaction = DateTime.Now;
                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        // Get All:

        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Transaction> listOfTransactions = await _context.Transactions.ToListAsync();
            return Ok(listOfTransactions);
        }

        // Get By ID:

        [HttpGet]
        public async Task<IHttpActionResult> GetByID([FromUri]int id)
        {
            Transaction foundTransaction = await _context.Transactions.FindAsync(id);
            if(foundTransaction == null)
            {
                return NotFound();
            }
            return Ok(foundTransaction);
        }

        // Put:

        [HttpPut]
        public async Task<IHttpActionResult> Put([FromUri]int id, [FromBody]Transaction model)
        {
            if(ModelState.IsValid)
            {
                if(id != model.ID)
                {
                    return BadRequest("Transaction ID Mismatch ...");
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
            var foundTransaction = await _context.Transactions.FindAsync(id);
            if(foundTransaction == null)
            {
                return NotFound();
            }
            _context.Transactions.Remove(foundTransaction);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
