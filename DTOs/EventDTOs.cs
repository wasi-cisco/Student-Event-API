using System.ComponentModel.DataAnnotations;

namespace StudentEvents.Api.DTOs
{
    public record EventCreateDto(
        [Required, MaxLength(150)] string Name,
        [Required, MaxLength(150)] string Venue,
        [Required] DateTime Date,
        string? Description
    );

    public record EventUpdateDto(
        [Required, MaxLength(150)] string Name,
        [Required, MaxLength(150)] string Venue,
        [Required] DateTime Date,
        string? Description
    );

    public record EventReadDto(
        int Id, string Name, string Venue, DateTime Date, string? Description
    );
}
