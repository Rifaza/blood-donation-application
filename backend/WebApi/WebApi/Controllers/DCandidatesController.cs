using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DCandidatesController : ControllerBase
    {
        private readonly DonationDbContext _context;

        public DCandidatesController(DonationDbContext context)
        {
            _context = context;
        }

        // GET: api/DCandidates
        // Added Search donors by blood group
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DCandidate>>> GetDCandidates(
            [FromQuery] string? bloodGroup,
            [FromQuery] string? search)
        {
            var query = _context.DCandidates.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(bloodGroup))
            {
                query = query.Where(d => d.BloodGroup == bloodGroup);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(d =>
                    d.FullName.Contains(search));
            }

            var candidates = await query.ToListAsync();

            return Ok(candidates);
        }

        // GET: api/DCandidates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DCandidate>> GetDCandidate(int id)
        {
            var dCandidate = await _context.DCandidates.FindAsync(id);

            if (dCandidate == null)
            {
                return NotFound();
            }

            return dCandidate;
        }

        // PUT: api/DCandidates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDCandidate(int id, DCandidate dCandidate)
        {
            dCandidate.Id = id;

            _context.Entry(dCandidate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DCandidateExists(id))
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

        // POST: api/DCandidates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DCandidate>> PostDCandidate(DCandidate dCandidate)
        {
            _context.DCandidates.Add(dCandidate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDCandidate", new { id = dCandidate.Id }, dCandidate);
        }

        // DELETE: api/DCandidates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDCandidate(int id)
        {
            var dCandidate = await _context.DCandidates.FindAsync(id);
            if (dCandidate == null)
            {
                return NotFound();
            }

            _context.DCandidates.Remove(dCandidate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DCandidateExists(int id)
        {
            return _context.DCandidates.Any(e => e.Id == id);
        }
    }
}
