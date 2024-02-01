using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiproj.Models;

namespace apiproj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Ace52024Context _context;

        public UserController(Ace52024Context context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HetalUsertable>>> GetHetalUsertable()
        {
            return await _context.HetalUsertable.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HetalUsertable>> GetHetalUsertable(string id)
        {
            var hetalUsertable = await _context.HetalUsertable.FindAsync(id);

            if (hetalUsertable == null)
            {
                return NotFound();
            }

            return hetalUsertable;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHetalUsertable(string id, HetalUsertable hetalUsertable)
        {
            if (id != hetalUsertable.Email)
            {
                return BadRequest();
            }

            _context.Entry(hetalUsertable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HetalUsertableExists(id))
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

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HetalUsertable>> PostHetalUsertable(HetalUsertable hetalUsertable)
        {
            _context.HetalUsertable.Add(hetalUsertable);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HetalUsertableExists(hetalUsertable.Email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetHetalUsertable", new { id = hetalUsertable.Email }, hetalUsertable);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHetalUsertable(string id)
        {
            var hetalUsertable = await _context.HetalUsertable.FindAsync(id);
            if (hetalUsertable == null)
            {
                return NotFound();
            }

            _context.HetalUsertable.Remove(hetalUsertable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HetalUsertableExists(string id)
        {
            return _context.HetalUsertable.Any(e => e.Email == id);
        }
    }
}
