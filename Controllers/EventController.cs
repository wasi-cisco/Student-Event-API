using Microsoft.AspNetCore.Mvc;
using StudentEvents.Api.DTOs;
using StudentEvents.Api.Services;

namespace StudentEvents.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _svc;
        public EventsController(IEventService svc) => _svc = svc;

        // GET /api/events  (upcoming events, optional venue filter)
        [HttpGet]
        public async Task<IActionResult> GetUpcoming([FromQuery] string? venue = null)
        {
            var data = await _svc.GetUpcomingAsync(venue);
            return Ok(data);
        }

        // GET /api/events/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var e = await _svc.GetByIdAsync(id);
            return e is null
                ? NotFound(new { message = $"Event with id {id} not found." })
                : Ok(e);
        }

        // POST /api/events
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EventCreateDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var created = await _svc.CreateAsync(dto);
            return CreatedAtAction(nameof(GetOne), new { id = created.Id }, created);
        }

        // PUT /api/events/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] EventUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var updated = await _svc.UpdateAsync(id, dto);
            return updated is null
                ? NotFound(new { message = $"Event with id {id} not found." })
                : Ok(updated);
        }

        // DELETE /api/events/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _svc.DeleteAsync(id);
            return ok
                ? Ok(new { message = $"Event with id {id} deleted successfully." })
                : NotFound(new { message = $"Event with id {id} not found." });
        }

        // GET /api/events/search?query=xyz
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest(new { message = "Query parameter is required." });

            var data = await _svc.SearchAsync(query);
            return Ok(data);
        }

        // GET /api/events/filter?sort=date|venue&venue=...
        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] string? sort, [FromQuery] string? venue)
        {
            var data = await _svc.FilterAsync(sort, venue);
            return Ok(data);
        }
    }
}
