using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Server.IRepository;
using SchoolProject.Shared.Domain;
using SchoolProject.Server.Data;
using SchoolProject.Server.IRepository;
using SchoolProject.Shared.Domain;

namespace SchoolProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CpusController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CpusController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/Cpus
        [HttpGet]
        public async Task<IActionResult> GetCpus()
        {
            var cpus = await _unitOfWork.Cpus.GetAll();
            return Ok(cpus);
        }

        // GET: api/Cpus/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCpu(int Id)
        {
            var cpu = await _unitOfWork.Cpus.Get(q => q.Id == Id);
            if (cpu == null)
            {
                return NotFound();
            }

            return Ok(cpu);
        }

        // PUT: api/Cpus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCpu(int id, Cpu cpu)
        {
            if (id != cpu.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Cpus.Update(cpu);

            try
            {
                await _unitOfWork.Save(HttpContext);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!await CpuExists(id))
                {
                    return NotFound();
                }
            }

            return NoContent();
        }

        // POST: api/Cpus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cpu>> PostCpu(Cpu cpu)
        {
            await _unitOfWork.Cpus.Insert(cpu);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetCpu", new { id = cpu.Id }, cpu);
        }

        // DELETE: api/Cpus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCpu(int id)
        {
            var cpu = await _unitOfWork.Cpus.Get(q => q.Id == id);
            if (cpu == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        private async Task<bool> CpuExists(int id)
        {
            var cpu = await _unitOfWork.Cpus.Get(q => q.Id == id);
            return cpu != null;
        }
    }
}
