using StudentEvents.Api.DTOs;
using StudentEvents.Api.Models;

namespace StudentEvents.Api.Services
{
    public interface IEventService
    {
        Task<IEnumerable<EventReadDto>> GetUpcomingAsync(string? venueFilter = null);
        Task<EventReadDto?> GetByIdAsync(int id);
        Task<EventReadDto> CreateAsync(EventCreateDto dto);
        Task<EventReadDto?> UpdateAsync(int id, EventUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<EventReadDto>> SearchAsync(string query);
        Task<IEnumerable<EventReadDto>> FilterAsync(string? sort, string? venue);
    }
}
