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
    public class OSsController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public OSsController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/OSs
        [HttpGet]
/*        public async Task<ActionResult<IEnumerable<OS>>> GetOSs()
        {
          if (_context.OSs == null)
          {
              return NotFound();
          }
            return await _context.OSs.ToListAsync();
        }*/
        public async Task<IActionResult> GetOSs()
        {
            var OSs = await _unitOfWork.OSs.GetAll();
            return Ok(OSs);
        }

        // GET: api/OSs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OS>> GetOS(int id)
        {
            var OS = await _unitOfWork.OSs.Get(q => q.Id == id);

            if (OS == null)
            {
                return NotFound();
            }

            return Ok(OS);
        }

        // PUT: api/OSs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOS(int id, OS OS)
        {
            if (id != OS.Id)
            {
                return BadRequest();
            }

            _unitOfWork.OSs.Update(OS);

            try
            {   
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await OSExists(id))
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

        // POST: api/OSs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OS>> PostOS(OS OS)
        {
            await _unitOfWork.OSs.Insert(OS);
            await _unitOfWork.Save(HttpContext);
            return CreatedAtAction("GetOS", new { id = OS.Id }, OS);
        }

        // DELETE: api/OSs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOS(int id)
        {
            var OS = await _unitOfWork.OSs.Get(q=>q.Id == id);
            if (OS == null) 
            { 
                return NotFound();
            }

            await _unitOfWork.OSs.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }


        private async Task<bool> OSExists(int id)
        {
            var make = await _unitOfWork.OSs.Get(q => q.Id == id);
            return make != null;

            //return (_context.Makes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
