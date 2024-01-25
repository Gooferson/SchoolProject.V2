using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Server.Data;
using SchoolProject.Server.IRepository;
using SchoolProject.Shared.Domain;
using SchoolProject.Server.Repository;

namespace SchoolProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaptopsController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public LaptopsController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Laptops
        [HttpGet]
/*        public async Task<ActionResult<IEnumerable<Laptop>>> GetLaptops()
        {
          if (_context.Laptops == null)
          {
              return NotFound();
          }
            return await _context.Laptops.ToListAsync();
        }*/
        public async Task<IActionResult> GetLaptops()
        {
            var Laptops = await _unitOfWork.Laptops.GetAll();
            return Ok(Laptops);
        }

        // GET: api/Laptops/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Laptop>> GetLaptop(int id)
        {
            var Laptop = await _unitOfWork.Laptops.Get(q => q.Id == id);

            if (Laptop == null)
            {
                return NotFound();
            }

            return Ok(Laptop);
        }

        // PUT: api/Laptops/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLaptop(int id, Laptop Laptop)
        {
            if (id != Laptop.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Laptops.Update(Laptop);

            try
            {   
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await LaptopExists(id))
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

        // POST: api/Laptops
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Laptop>> PostLaptop(Laptop Laptop)
        {
            await _unitOfWork.Laptops.Insert(Laptop);
            await _unitOfWork.Save(HttpContext);
            return CreatedAtAction("GetLaptop", new { id = Laptop.Id }, Laptop);
        }

        // DELETE: api/Laptops/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLaptop(int id)
        {
            var Laptop = await _unitOfWork.Laptops.Get(q=>q.Id == id);
            if (Laptop == null) 
            { 
                return NotFound();
            }

            await _unitOfWork.Laptops.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }


        private async Task<bool> LaptopExists(int id)
        {
            var make = await _unitOfWork.Laptops.Get(q => q.Id == id);
            return make != null;

            //return (_context.Makes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
