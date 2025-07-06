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

        [Authorize(Roles = "Admin,Organizer,Attendee")]
        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _context.Events
                .Include(e => e.Venue)
                .Include(e => e.Category)
                .Select(e => new EventDto
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    ImageUrl = e.ImageUrl,
                    Venue = new VenueDto
                    {
                        Id = e.Venue.Id,
                        Name = e.Venue.Name
                    },
                    Category = new CategoryDto
                    {
                        Id = e.Category.Id,
                        Name = e.Category.Name
                    }
                })
                .ToListAsync();

            return Ok(events);
        }


        // GET: api/event/5
        //[AllowAnonymous]
        [Authorize(Roles = "Admin,Organizer,Attendee")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var ev = await _context.Events
                .Include(e => e.Venue)
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (ev == null)
                return NotFound();

            var eventDto = new EventDto
            {
                Id = ev.Id,
                Title = ev.Title,
                Description = ev.Description,
                StartDate = ev.StartDate,
                EndDate = ev.EndDate,
                ImageUrl = ev.ImageUrl,
                Venue = new VenueDto
                {
                    Id = ev.Venue!.Id,
                    Name = ev.Venue.Name
                },
                Category = new CategoryDto
                {
                    Id = ev.Category!.Id,
                    Name = ev.Category.Name
                }
            };

            return Ok(eventDto);
        }



        // POST: api/event
       // [Authorize(Roles = "Organizer")]
        [Authorize(Roles = "Admin,Organizer,Attendee")]
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
        //[Authorize(Roles = "Organizer")]
        [Authorize(Roles = "Admin,Organizer")]
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

        [Authorize(Roles = "Admin,Organizer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] CreateEventDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var eventEntity = await _context.Events.FindAsync(id);

            if (eventEntity == null)
                return NotFound("Event not found.");

            // Optional: restrict edit to the creator only
            if (eventEntity.OrganizerId != userId && !User.IsInRole("Admin"))
                return Forbid("Only the creator or an admin can update this event.");

            eventEntity.Title = dto.Title;
            eventEntity.Description = dto.Description;
            eventEntity.StartDate = dto.StartDate;
            eventEntity.EndDate = dto.EndDate;
            eventEntity.ImageUrl = dto.ImageUrl;
            eventEntity.VenueId = dto.VenueId;
            eventEntity.CategoryId = dto.CategoryId;

            await _context.SaveChangesAsync();

            return Ok(eventEntity);
        }


    }
}