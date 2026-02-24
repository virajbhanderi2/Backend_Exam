using Backend_Exam.Data;
using Backend_Exam.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Backend_Exam.Controllers
{
    [ApiController]
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CommentsController(AppDbContext context)
        {
            _context = context;
        }

        private int UserId =>
            int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        private string UserRole =>
            User.FindFirstValue(ClaimTypes.Role)!;

        private object Format(TicketComment c) => new
        {
            c.Id,
            comment = c.Comment,
            user = c.User == null ? null : new
            {
                c.User.Id,
                c.User.Name,
                c.User.Email,
                role = c.User.Role?.Name,
                c.User.CreatedAt
            },
            c.CreatedAt
        };

        private bool HasAccess(Ticket ticket)
        {
            if (UserRole == "MANAGER") return true;
            if (UserRole == "SUPPORT" && ticket.AssignedTo == UserId) return true;
            if (UserRole == "USER" && ticket.CreatedBy == UserId) return true;
            return false;
        }

        [HttpPost("tickets/{id}/comments")]
        public async Task<IActionResult> Add(int id, CommentDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
                return NotFound(new { message = "Ticket not found" });

            if (!HasAccess(ticket))
                return Forbid();

            var comment = new TicketComment
            {
                TicketId = id,
                UserId = UserId,
                Comment = dto.Comment,
                CreatedAt = DateTime.UtcNow
            };

            _context.TicketComments.Add(comment);
            await _context.SaveChangesAsync();

            var created = await _context.TicketComments
                .Include(c => c.User).ThenInclude(u => u.Role)
                .FirstAsync(c => c.Id == comment.Id);

            return Created("", Format(created));
        }

        [HttpGet("tickets/{id}/comments")]
        public async Task<IActionResult> GetAll(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
                return NotFound(new { message = "Ticket not found" });

            if (!HasAccess(ticket))
                return Forbid();

            var comments = await _context.TicketComments
                .Where(c => c.TicketId == id)
                .Include(c => c.User).ThenInclude(u => u.Role)
                .OrderBy(c => c.CreatedAt)
                .ToListAsync();

            return Ok(comments.Select(Format));
        }

        [HttpPatch("comments/{id}")]
        public async Task<IActionResult> Edit(int id, CommentDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _context.TicketComments
                .Include(c => c.User).ThenInclude(u => u.Role)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
                return NotFound(new { message = "Comment not found" });

            if (UserRole != "MANAGER" && comment.UserId != UserId)
                return Forbid();

            comment.Comment = dto.Comment;
            await _context.SaveChangesAsync();

            return Ok(Format(comment));
        }

        [HttpDelete("comments/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _context.TicketComments.FindAsync(id);
            if (comment == null)
                return NotFound(new { message = "Comment not found" });

            if (UserRole != "MANAGER" && comment.UserId != UserId)
                return Forbid();

            _context.TicketComments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}