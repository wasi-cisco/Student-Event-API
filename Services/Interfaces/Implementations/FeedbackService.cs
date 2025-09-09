using Microsoft.EntityFrameworkCore;
using StudentEvents.Api.Data;
using StudentEvents.Api.DTOs;
using StudentEvents.Api.Models;

namespace StudentEvents.Api.Services.Implementations
{
    public class FeedbackService : IFeedbackService
    {
        private readonly AppDbContext _db;
        public FeedbackService(AppDbContext db) => _db = db;

        public async Task<(bool ok, string message)> SubmitAsync(FeedbackCreateDto dto)
        {
            var evnt = await _db.Events.FirstOrDefaultAsync(e => e.Id == dto.EventId);
            if (evnt == null) return (false, "Event not found.");
            if (evnt.Date > DateTime.UtcNow) return (false, "Feedback allowed only after the event.");

            var student = await _db.Students.FirstOrDefaultAsync(s => s.Email == dto.StudentEmail);
            if (student == null) return (false, "Student not found.");

            var registered = await _db.Registrations.AnyAsync(r => r.StudentId == student.Id && r.EventId == evnt.Id);
            if (!registered) return (false, "Only registered students can leave feedback.");

            _db.Feedbacks.Add(new Feedback
            {
                EventId = evnt.Id,
                StudentId = student.Id,
                Rating = dto.Rating,
                Comment = dto.Comment
            });

            await _db.SaveChangesAsync();
            return (true, "Feedback submitted.");
        }
    }
}
