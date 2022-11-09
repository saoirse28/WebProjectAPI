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
    public class CommentReactionsController : ControllerBase
    {
        private readonly ProjectApiDBContext _context;

        public CommentReactionsController(ProjectApiDBContext context)
        {
            _context = context;
        }

        // GET: api/CommentReactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentReactions>>> GetCommentReactions()
        {
            return await _context.CommentReactions.ToListAsync();
        }

        // GET: api/CommentReactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentReactions>> GetCommentReactions(int id)
        {
            var commentReactions = await _context.CommentReactions.FindAsync(id);

            if (commentReactions == null)
            {
                return NotFound();
            }

            return commentReactions;
        }

        // PUT: api/CommentReactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommentReactions(int id, CommentReactions commentReactions)
        {
            if (id != commentReactions.CommentId)
            {
                return BadRequest();
            }

            _context.Entry(commentReactions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentReactionsExists(id))
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

        // POST: api/CommentReactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CommentReactions>> PostCommentReactions(CommentReactions commentReactions)
        {
            _context.CommentReactions.Add(commentReactions);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CommentReactionsExists(commentReactions.CommentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCommentReactions", new { id = commentReactions.CommentId }, commentReactions);
        }

        // DELETE: api/CommentReactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommentReactions(int id)
        {
            var commentReactions = await _context.CommentReactions.FindAsync(id);
            if (commentReactions == null)
            {
                return NotFound();
            }

            _context.CommentReactions.Remove(commentReactions);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentReactionsExists(int id)
        {
            return _context.CommentReactions.Any(e => e.CommentId == id);
        }
    }
}
