using StudentEvents.Api.DTOs;
using StudentEvents.Api.Models;

namespace StudentEvents.Api.Services
{
    public interface IFeedbackService
    {
        Task<(bool ok, string message)> SubmitAsync(FeedbackCreateDto dto);
    }
}
