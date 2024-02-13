using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiproj.Models;
using apiproj.Services;

namespace apiproj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightServ<HetalFlight> flserv;

        public FlightController(IFlightServ<HetalFlight> _flserv)
        {
            flserv = _flserv;
        }

        // GET: api/Flight
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HetalFlight>>> GetHetalFlights()
        {
            return flserv.ShowFlights(); 
            //
        }

        // GET: api/Flight/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HetalFlight>> GetHetalFlight(int id)
        {
            var flight = flserv.GetFlightById(id);

            if (flight == null)
            {
                return NotFound();
            }

            return flight;
        }

        // PUT: api/Flight/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHetalFlight(int id, HetalFlight hetalFlight)
        {
            //Console.WriteLine(hetalFlight.Rate);
            if (id != hetalFlight.FlightId)
            {
                return BadRequest();
            }

            try
            {
                flserv.EditFlight(id,hetalFlight);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HetalFlightExists(id))
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

        // POST: api/Flight
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HetalFlight>> PostHetalFlight(HetalFlight hetalFlight)
        {
            try
            {
                flserv.AddFlight(hetalFlight);
            }
            catch (DbUpdateException)
            {
                if (HetalFlightExists(hetalFlight.FlightId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetHetalFlight", new { id = hetalFlight.FlightId }, hetalFlight);
        }

        // DELETE: api/Flight/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHetalFlight(int id)
        {
            var hetalFlight = flserv.GetFlightById(id);
            if (hetalFlight == null)
            {
                return NotFound();
            }

            flserv.DeleteFlight(id);

            return NoContent();
        }

        private bool HetalFlightExists(int id)
        {
            HetalFlight fl = flserv.GetFlightById(id);
            if(fl!=null)
            return true;
            else
            return false;
        }
    }
}
