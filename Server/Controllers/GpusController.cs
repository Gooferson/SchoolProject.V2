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
    public class GpusController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public GpusController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Gpus
        [HttpGet]
/*        public async Task<ActionResult<IEnumerable<Gpu>>> GetGpus()
        {
          if (_context.Gpus == null)
          {
              return NotFound();
          }
            return await _context.Gpus.ToListAsync();
        }*/
        public async Task<IActionResult> GetGpus()
        {
            var Gpus = await _unitOfWork.Gpus.GetAll();
            return Ok(Gpus);
        }

        // GET: api/Gpus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gpu>> GetGpu(int id)
        {
            var Gpu = await _unitOfWork.Gpus.Get(q => q.Id == id);

            if (Gpu == null)
            {
                return NotFound();
            }

            return Ok(Gpu);
        }

        // PUT: api/Gpus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGpu(int id, Gpu Gpu)
        {
            if (id != Gpu.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Gpus.Update(Gpu);

            try
            {   
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await GpuExists(id))
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

        // POST: api/Gpus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Gpu>> PostGpu(Gpu Gpu)
        {
            await _unitOfWork.Gpus.Insert(Gpu);
            await _unitOfWork.Save(HttpContext);
            return CreatedAtAction("GetGpu", new { id = Gpu.Id }, Gpu);
        }

        // DELETE: api/Gpus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGpu(int id)
        {
            var Gpu = await _unitOfWork.Gpus.Get(q=>q.Id == id);
            if (Gpu == null) 
            { 
                return NotFound();
            }

            await _unitOfWork.Gpus.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }


        private async Task<bool> GpuExists(int id)
        {
            var make = await _unitOfWork.Gpus.Get(q => q.Id == id);
            return make != null;

            //return (_context.Makes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
