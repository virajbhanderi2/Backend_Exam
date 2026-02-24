using Backend_Exam.Data;
using Backend_Exam.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Backend_Exam.Controllers
{
    [ApiController]
    [Route("tickets")]
    [Authorize]
    public class TicketsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TicketsController(AppDbContext context)
        {
            _context = context;
        }

        private int UserId =>
            int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        private string UserRole =>
            User.FindFirstValue(ClaimTypes.Role)!;

        private IQueryable<Ticket> BaseQuery()
        {
            return _context.Tickets
                .Include(t => t.CreatedByUser).ThenInclude(u => u.Role)
                .Include(t => t.AssignedToUser).ThenInclude(u => u!.Role);
        }

        private object Format(Ticket t) => new
        {
            t.Id,
            t.Title,
            t.Description,
            status = t.Status.ToString(),
            priority = t.Priority.ToString(),
            created_by = t.CreatedByUser == null ? null : new
            {
                t.CreatedByUser.Id,
                t.CreatedByUser.Name,
                t.CreatedByUser.Email,
                role = t.CreatedByUser.Role?.Name,
                t.CreatedByUser.CreatedAt
            },
            assigned_to = t.AssignedToUser == null ? null : new
            {
                t.AssignedToUser.Id,
                t.AssignedToUser.Name,
                t.AssignedToUser.Email,
                role = t.AssignedToUser.Role?.Name,
                t.AssignedToUser.CreatedAt
            },
            t.CreatedAt
        };

        [HttpPost]
        [Authorize(Roles = "USER,MANAGER")]
        public async Task<IActionResult> Create(CreateTicketDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ticket = new Ticket
            {
                Title = dto.Title,
                Description = dto.Description,
                Priority = dto.Priority,
                Status = TicketStatus.OPEN,
                CreatedBy = UserId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            var created = await BaseQuery()
                .FirstAsync(t => t.Id == ticket.Id);

            return Created("", Format(created));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = BaseQuery();

            if (UserRole == "SUPPORT")
                query = query.Where(t => t.AssignedTo == UserId);
            else if (UserRole == "USER")
                query = query.Where(t => t.CreatedBy == UserId);

            var tickets = await query.ToListAsync();
            return Ok(tickets.Select(Format));
        }

        [HttpPatch("{id}/assign")]
        [Authorize(Roles = "MANAGER,SUPPORT")]
        public async Task<IActionResult> Assign(int id, AssignDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ticket = await BaseQuery()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (ticket == null)
                return NotFound(new { message = "Ticket not found" });

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == dto.UserId);

            if (user == null)
                return NotFound(new { message = "User not found" });

            if (user.Role.Name == "USER")
                return BadRequest(new { message = "Cannot assign to USER role" });

            ticket.AssignedTo = dto.UserId;
            await _context.SaveChangesAsync();

            var updated = await BaseQuery()
                .FirstAsync(t => t.Id == id);

            return Ok(Format(updated));
        }

        [HttpPatch("{id}/status")]
        [Authorize(Roles = "MANAGER,SUPPORT")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateStatusDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ticket = await BaseQuery()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (ticket == null)
                return NotFound(new { message = "Ticket not found" });

            if (!IsValidTransition(ticket.Status, dto.Status))
                return BadRequest(new { message = "Invalid status transition" });

            var oldStatus = ticket.Status;
            ticket.Status = dto.Status;

            _context.TicketStatusLogs.Add(new TicketStatusLog
            {
                TicketId = ticket.Id,
                OldStatus = oldStatus,
                NewStatus = dto.Status,
                ChangedBy = UserId,
                ChangedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return Ok(Format(ticket));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "MANAGER")]
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
                return NotFound(new { message = "Ticket not found" });

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private static bool IsValidTransition(TicketStatus current, TicketStatus next)
        {
            return (current, next) switch
            {
                (TicketStatus.OPEN, TicketStatus.IN_PROGRESS) => true,
                (TicketStatus.IN_PROGRESS, TicketStatus.RESOLVED) => true,
                (TicketStatus.RESOLVED, TicketStatus.CLOSED) => true,
                _ => false
            };
        }
    }
}