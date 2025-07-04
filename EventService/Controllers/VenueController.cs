// VenueController.cs - Updated to use DTO
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using EventService.Data;
using EventService.Models;

namespace EventService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VenueController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VenueController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _context.Venues.ToListAsync());

        [HttpPost]
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Create([FromBody] CreateVenueDto dto)
        {
            var venue = new Venue
            {
                Name = dto.Name,
                Address = dto.Address
            };

            _context.Venues.Add(venue);
            await _context.SaveChangesAsync();
            return Ok(venue);
        }
    }
}