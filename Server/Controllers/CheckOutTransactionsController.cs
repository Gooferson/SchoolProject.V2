using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Server.Data;
using SchoolProject.Server.data.IRepository;
using SchoolProject.Shared.Domain;
using SchoolProject.Server.Data.IRepository;

namespace SchoolProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckOutTransactionsController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public CheckOutTransactionsController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/CheckOutTransactions
        [HttpGet]
/*        public async Task<ActionResult<IEnumerable<CheckOutTransaction>>> GetCheckOutTransactions()
        {
          if (_context.CheckOutTransactions == null)
          {
              return NotFound();
          }
            return await _context.CheckOutTransactions.ToListAsync();
        }*/
        public async Task<IActionResult> GetCheckOutTransactions()
        {
            var CheckOutTransactions = await _unitOfWork.CheckOutTransactions.GetAll();
            return Ok(CheckOutTransactions);
        }

        // GET: api/CheckOutTransactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CheckOutTransaction>> GetCheckOutTransaction(int id)
        {
            var CheckOutTransaction = await _unitOfWork.CheckOutTransactions.Get(q => q.Id == id);

            if (CheckOutTransaction == null)
            {
                return NotFound();
            }

            return Ok(CheckOutTransaction);
        }

        // PUT: api/CheckOutTransactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCheckOutTransaction(int id, CheckOutTransaction CheckOutTransaction)
        {
            if (id != CheckOutTransaction.Id)
            {
                return BadRequest();
            }

            _unitOfWork.CheckOutTransactions.Update(CheckOutTransaction);

            try
            {   
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CheckOutTransactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CheckOutTransactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CheckOutTransaction>> PostCheckOutTransaction(CheckOutTransaction CheckOutTransaction)
        {
            await _unitOfWork.CheckOutTransactions.Insert(CheckOutTransaction);
            await _unitOfWork.Save(HttpContext);
            return CreatedAtAction("GetCheckOutTransaction", new { id = CheckOutTransaction.Id }, CheckOutTransaction);
        }

        // DELETE: api/CheckOutTransactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCheckOutTransaction(int id)
        {
            var CheckOutTransaction = await _unitOfWork.CheckOutTransactions.Get(q=>q.Id == id);
            if (CheckOutTransaction == null) 
            { 
                return NotFound();
            }

            await _unitOfWork.CheckOutTransactions.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }


        private async Task<bool> CheckOutTransactionExists(int id)
        {
            var make = await _unitOfWork.CheckOutTransactions.Get(q => q.Id == id);
            return make != null;

            //return (_context.Makes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
