using System.ComponentModel.DataAnnotations;

namespace StudentEvents.Api.DTOs
{
    // We’ll register by student email (create if not exists) to make testing easy
    public record RegistrationRequestDto(
        [Required, EmailAddress] string StudentEmail,
        [Required, MaxLength(120)] string StudentName,
        [Required] int EventId
    );
}
