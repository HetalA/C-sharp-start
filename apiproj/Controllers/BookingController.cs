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
    public class BookingController : ControllerBase
    {
        private readonly Ace52024Context _context;

        public BookingController(Ace52024Context context)
        {
            _context = context;
        }

        // GET: api/Booking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HetalBooking>>> GetHetalBooking()
        {
            return await _context.HetalBookings.ToListAsync();
        }

        // GET: api/Booking/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HetalBooking>> GetHetalBooking(int id)
        {
            var hetalBooking = await _context.HetalBookings.FindAsync(id);
            Console.WriteLine(hetalBooking);
            if (hetalBooking == null)
            {
                return NotFound();
            }

            return hetalBooking;
        }

        // PUT: api/Booking/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHetalBooking(int id, HetalBooking hetalBooking)
        {
            if (id != hetalBooking.BookingId)
            {
                return BadRequest();
            }

            _context.Entry(hetalBooking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HetalBookingExists(id))
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

        // POST: api/Booking
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HetalBooking>> PostHetalBooking(HetalBooking hetalBooking)
        {
            _context.HetalBookings.Add(hetalBooking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHetalBooking", new { id = hetalBooking.BookingId }, hetalBooking);
        }

        // DELETE: api/Booking/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHetalBooking(int id)
        {
            var hetalBooking = await _context.HetalBookings.FindAsync(id);
            if (hetalBooking == null)
            {
                return NotFound();
            }

            _context.HetalBookings.Remove(hetalBooking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HetalBookingExists(int id)
        {
            return _context.HetalBookings.Any(e => e.BookingId == id);
        }
    }
}
