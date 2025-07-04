// RegistrationController.cs - No DTOs needed (route param only)
using EventService.Data;
using EventService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class RegistrationController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public RegistrationController(ApplicationDbContext context) => _context = context;

    [Authorize(Roles = "Attendee")]
    [HttpPost("register/{eventId}")]
    public async Task<IActionResult> RegisterForEvent(int eventId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var alreadyRegistered = await _context.Registrations
            .AnyAsync(r => r.AttendeeId == userId && r.EventId == eventId);

        if (alreadyRegistered)
            return BadRequest("Already registered.");

        var registration = new Registration
        {
            EventId = eventId,
            AttendeeId = userId,
            RegisteredAt = DateTime.UtcNow
        };

        _context.Registrations.Add(registration);
        await _context.SaveChangesAsync();
        return Ok("Registered successfully.");
    }

    [Authorize(Roles = "Attendee")]
    [HttpGet("my-events")]
    public async Task<IActionResult> GetMyEvents()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var events = await _context.Registrations
            .Include(r => r.Event)
            .ThenInclude(e => e.Venue)
            .Include(r => r.Event)
            .ThenInclude(e => e.Category)
            .Where(r => r.AttendeeId == userId)
            .Select(r => r.Event)
            .ToListAsync();

        return Ok(events);
    }
}
