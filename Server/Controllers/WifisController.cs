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
    public class CartsController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public CartsController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Carts
        [HttpGet]
/*        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
          if (_context.Carts == null)
          {
              return NotFound();
          }
            return await _context.Carts.ToListAsync();
        }*/
        public async Task<IActionResult> GetCarts()
        {
            var Carts = await _unitOfWork.Carts.GetAll();
            return Ok(Carts);
        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCart(int id)
        {
            var Cart = await _unitOfWork.Carts.Get(q => q.Id == id);

            if (Cart == null)
            {
                return NotFound();
            }

            return Ok(Cart);
        }

        // PUT: api/Carts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart(int id, Cart Cart)
        {
            if (id != Cart.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Carts.Update(Cart);

            try
            {   
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CartExists(id))
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

        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cart>> PostCart(Cart Cart)
        {
            await _unitOfWork.Carts.Insert(Cart);
            await _unitOfWork.Save(HttpContext);
            return CreatedAtAction("GetCart", new { id = Cart.Id }, Cart);
        }

        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var Cart = await _unitOfWork.Carts.Get(q=>q.Id == id);
            if (Cart == null) 
            { 
                return NotFound();
            }

            await _unitOfWork.Carts.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }


        private async Task<bool> CartExists(int id)
        {
            var make = await _unitOfWork.Carts.Get(q => q.Id == id);
            return make != null;

            //return (_context.Makes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
