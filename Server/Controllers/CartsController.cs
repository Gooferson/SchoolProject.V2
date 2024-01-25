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
    public class WifisController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public WifisController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Wifis
        [HttpGet]
/*        public async Task<ActionResult<IEnumerable<Wifi>>> GetWifis()
        {
          if (_context.Wifis == null)
          {
              return NotFound();
          }
            return await _context.Wifis.ToListAsync();
        }*/
        public async Task<IActionResult> GetWifis()
        {
            var Wifis = await _unitOfWork.Wifis.GetAll();
            return Ok(Wifis);
        }

        // GET: api/Wifis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Wifi>> GetWifi(int id)
        {
            var Wifi = await _unitOfWork.Wifis.Get(q => q.Id == id);

            if (Wifi == null)
            {
                return NotFound();
            }

            return Ok(Wifi);
        }

        // PUT: api/Wifis/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWifi(int id, Wifi Wifi)
        {
            if (id != Wifi.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Wifis.Update(Wifi);

            try
            {   
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await WifiExists(id))
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

        // POST: api/Wifis
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Wifi>> PostWifi(Wifi Wifi)
        {
            await _unitOfWork.Wifis.Insert(Wifi);
            await _unitOfWork.Save(HttpContext);
            return CreatedAtAction("GetWifi", new { id = Wifi.Id }, Wifi);
        }

        // DELETE: api/Wifis/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWifi(int id)
        {
            var Wifi = await _unitOfWork.Wifis.Get(q=>q.Id == id);
            if (Wifi == null) 
            { 
                return NotFound();
            }

            await _unitOfWork.Wifis.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }


        private async Task<bool> WifiExists(int id)
        {
            var make = await _unitOfWork.Wifis.Get(q => q.Id == id);
            return make != null;

            //return (_context.Makes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
