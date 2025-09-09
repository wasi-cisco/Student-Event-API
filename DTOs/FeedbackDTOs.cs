using System.ComponentModel.DataAnnotations;

namespace StudentEvents.Api.DTOs
{
    public record FeedbackCreateDto(
        [Required] int EventId,
        [Required, EmailAddress] string StudentEmail,
        [Range(1,5)] int Rating,
        string? Comment
    );
}
