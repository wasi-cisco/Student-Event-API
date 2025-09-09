using Microsoft.EntityFrameworkCore;
using StudentEvents.Api.Data;
using StudentEvents.Api.DTOs;
using StudentEvents.Api.Models;

namespace StudentEvents.Api.Services.Implementations
{
    public class RegistrationService : IRegistrationService
    {
        private readonly AppDbContext _db;
        public RegistrationService(AppDbContext db) => _db = db;

        public async Task<bool> RegisterAsync(RegistrationRequestDto dto)
        {
            var evnt = await _db.Events.FirstOrDefaultAsync(e => e.Id == dto.EventId);
            if (evnt == null || evnt.Date < DateTime.UtcNow) return false;

            var student = await _db.Students.FirstOrDefaultAsync(s => s.Email == dto.StudentEmail);
            if (student == null)
            {
                student = new Student { Email = dto.StudentEmail, Name = dto.StudentName };
                _db.Students.Add(student);
                await _db.SaveChangesAsync();
            }

            var already = await _db.Registrations.AnyAsync(r => r.StudentId == student.Id && r.EventId == evnt.Id);
            if (already) return false;

            _db.Registrations.Add(new Registration { StudentId = student.Id, EventId = evnt.Id });
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
