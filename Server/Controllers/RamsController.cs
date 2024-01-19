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
    public class RamsController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public RamsController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Rams
        [HttpGet]
/*        public async Task<ActionResult<IEnumerable<Ram>>> GetRams()
        {
          if (_context.Rams == null)
          {
              return NotFound();
          }
            return await _context.Rams.ToListAsync();
        }*/
        public async Task<IActionResult> GetRams()
        {
            var Rams = await _unitOfWork.Rams.GetAll();
            return Ok(Rams);
        }

        // GET: api/Rams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ram>> GetRam(int id)
        {
            var Ram = await _unitOfWork.Rams.Get(q => q.Id == id);

            if (Ram == null)
            {
                return NotFound();
            }

            return Ok(Ram);
        }

        // PUT: api/Rams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRam(int id, Ram Ram)
        {
            if (id != Ram.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Rams.Update(Ram);

            try
            {   
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await RamExists(id))
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

        // POST: api/Rams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ram>> PostRam(Ram Ram)
        {
            await _unitOfWork.Rams.Insert(Ram);
            await _unitOfWork.Save(HttpContext);
            return CreatedAtAction("GetRam", new { id = Ram.Id }, Ram);
        }

        // DELETE: api/Rams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRam(int id)
        {
            var Ram = await _unitOfWork.Rams.Get(q=>q.Id == id);
            if (Ram == null) 
            { 
                return NotFound();
            }

            await _unitOfWork.Rams.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }


        private async Task<bool> RamExists(int id)
        {
            var make = await _unitOfWork.Rams.Get(q => q.Id == id);
            return make != null;

            //return (_context.Makes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
