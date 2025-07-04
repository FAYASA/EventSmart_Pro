// EventController.cs - Updated to support 1 Category per Event
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EventService.Data;
using EventService.Models;

namespace EventService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/event
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _context.Events
                .Include(e => e.Venue)
                .Include(e => e.Category)
                .ToListAsync();
            return Ok(events);
        }

        // GET: api/event/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var e = await _context.Events
                .Include(ev => ev.Venue)
                .Include(ev => ev.Category)
                .FirstOrDefaultAsync(ev => ev.Id == id);

            if (e == null)
                return NotFound();

            return Ok(e);
        }

        // POST: api/event
        [Authorize(Roles = "Organizer")]
        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto dto)
        {
            var userId =  User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Fetch related entities from the database
            //var venue = await _context.Venues.FindAsync(dto.VenueId);
            //var category = await _context.Categories.FindAsync(dto.CategoryId);
            //var organizer = await _context.Users.FindAsync(userId);


            var eventEntity = new Event
            {
                Title = dto.Title,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                ImageUrl = dto.ImageUrl,
                VenueId = dto.VenueId,
                CategoryId = dto.CategoryId,
                OrganizerId = userId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Events.Add(eventEntity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEvent), new { id = eventEntity.Id }, eventEntity);
        }

        // DELETE: api/event/5
        [Authorize(Roles = "Organizer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var evt = await _context.Events.FindAsync(id);
            if (evt == null) return NotFound();

            if (evt.OrganizerId != User.FindFirstValue(ClaimTypes.NameIdentifier))
                return Forbid();

            _context.Events.Remove(evt);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}