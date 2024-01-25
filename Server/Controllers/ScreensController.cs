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
    public class ScreensController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public ScreensController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Screens
        [HttpGet]
/*        public async Task<ActionResult<IEnumerable<Screen>>> GetScreens()
        {
          if (_context.Screens == null)
          {
              return NotFound();
          }
            return await _context.Screens.ToListAsync();
        }*/
        public async Task<IActionResult> GetScreens()
        {
            var Screens = await _unitOfWork.Screens.GetAll();
            return Ok(Screens);
        }

        // GET: api/Screens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Screen>> GetScreen(int id)
        {
            var Screen = await _unitOfWork.Screens.Get(q => q.Id == id);

            if (Screen == null)
            {
                return NotFound();
            }

            return Ok(Screen);
        }

        // PUT: api/Screens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScreen(int id, Screen Screen)
        {
            if (id != Screen.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Screens.Update(Screen);

            try
            {   
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ScreenExists(id))
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

        // POST: api/Screens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Screen>> PostScreen(Screen Screen)
        {
            await _unitOfWork.Screens.Insert(Screen);
            await _unitOfWork.Save(HttpContext);
            return CreatedAtAction("GetScreen", new { id = Screen.Id }, Screen);
        }

        // DELETE: api/Screens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScreen(int id)
        {
            var Screen = await _unitOfWork.Screens.Get(q=>q.Id == id);
            if (Screen == null) 
            { 
                return NotFound();
            }

            await _unitOfWork.Screens.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }


        private async Task<bool> ScreenExists(int id)
        {
            var make = await _unitOfWork.Screens.Get(q => q.Id == id);
            return make != null;

            //return (_context.Makes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
