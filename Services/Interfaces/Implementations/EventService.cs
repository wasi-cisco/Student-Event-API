using Microsoft.EntityFrameworkCore;
using StudentEvents.Api.Data;
using StudentEvents.Api.DTOs;
using StudentEvents.Api.Models;

namespace StudentEvents.Api.Services.Implementations
{
    public class EventService : IEventService
    {
        private readonly AppDbContext _db;
        public EventService(AppDbContext db) => _db = db;

        public async Task<IEnumerable<EventReadDto>> GetUpcomingAsync(string? venueFilter = null)
        {
            var now = DateTime.UtcNow;
            var query = _db.Events.AsNoTracking().Where(e => e.Date >= now);

            if (!string.IsNullOrEmpty(venueFilter))
                query = query.Where(e => e.Venue == venueFilter);

            return await query
                .OrderBy(e => e.Date)
                .Select(e => new EventReadDto(e.Id, e.Name, e.Venue, e.Date, e.Description))
                .ToListAsync();
        }

        public async Task<EventReadDto?> GetByIdAsync(int id) =>
            await _db.Events
                .Where(e => e.Id == id)
                .Select(e => new EventReadDto(e.Id, e.Name, e.Venue, e.Date, e.Description))
                .FirstOrDefaultAsync();

        public async Task<EventReadDto> CreateAsync(EventCreateDto dto)
        {
            var entity = new Event { Name = dto.Name, Venue = dto.Venue, Date = dto.Date, Description = dto.Description };
            _db.Events.Add(entity);
            await _db.SaveChangesAsync();
            return new EventReadDto(entity.Id, entity.Name, entity.Venue, entity.Date, entity.Description);
        }

        public async Task<EventReadDto?> UpdateAsync(int id, EventUpdateDto dto)
        {
            var entity = await _db.Events.FindAsync(id);
            if (entity == null) return null;

            entity.Name = dto.Name;
            entity.Venue = dto.Venue;
            entity.Date = dto.Date;
            entity.Description = dto.Description;

            await _db.SaveChangesAsync();
            return new EventReadDto(entity.Id, entity.Name, entity.Venue, entity.Date, entity.Description);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _db.Events.FindAsync(id);
            if (entity == null) return false;

            _db.Events.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<EventReadDto>> SearchAsync(string query) =>
            await _db.Events
                .Where(e => EF.Functions.Like(e.Name, $"%{query}%") || EF.Functions.Like(e.Venue, $"%{query}%"))
                .OrderBy(e => e.Date)
                .Select(e => new EventReadDto(e.Id, e.Name, e.Venue, e.Date, e.Description))
                .ToListAsync();

        public async Task<IEnumerable<EventReadDto>> FilterAsync(string? sort, string? venue)
        {
            var q = _db.Events.AsQueryable();

            if (!string.IsNullOrEmpty(venue))
                q = q.Where(e => e.Venue == venue);

            q = sort?.ToLower() switch
            {
                "date"  => q.OrderBy(e => e.Date),
                "venue" => q.OrderBy(e => e.Venue).ThenBy(e => e.Date),
                _       => q.OrderBy(e => e.Id)
            };

            return await q.Select(e => new EventReadDto(e.Id, e.Name, e.Venue, e.Date, e.Description)).ToListAsync();
        }
    }
}
