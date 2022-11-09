using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProjectAPI.Data;
using WebProjectAPI.Data.Entities;

namespace WebProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoReactionsController : ControllerBase
    {
        private readonly ProjectApiDBContext _context;

        public PhotoReactionsController(ProjectApiDBContext context)
        {
            _context = context;
        }

        // GET: api/PhotoReactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhotoReactions>>> GetPhotoReactions()
        {
            return await _context.PhotoReactions.ToListAsync();
        }

        // GET: api/PhotoReactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhotoReactions>> GetPhotoReactions(int id)
        {
            var photoReactions = await _context.PhotoReactions.FindAsync(id);

            if (photoReactions == null)
            {
                return NotFound();
            }

            return photoReactions;
        }

        // PUT: api/PhotoReactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhotoReactions(int id, PhotoReactions photoReactions)
        {
            if (id != photoReactions.ReactionId)
            {
                return BadRequest();
            }

            _context.Entry(photoReactions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoReactionsExists(id))
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

        // POST: api/PhotoReactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PhotoReactions>> PostPhotoReactions(PhotoReactions photoReactions)
        {
            _context.PhotoReactions.Add(photoReactions);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PhotoReactionsExists(photoReactions.ReactionId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPhotoReactions", new { id = photoReactions.ReactionId }, photoReactions);
        }

        // DELETE: api/PhotoReactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhotoReactions(int id)
        {
            var photoReactions = await _context.PhotoReactions.FindAsync(id);
            if (photoReactions == null)
            {
                return NotFound();
            }

            _context.PhotoReactions.Remove(photoReactions);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhotoReactionsExists(int id)
        {
            return _context.PhotoReactions.Any(e => e.ReactionId == id);
        }
    }
}
