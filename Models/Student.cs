using System.ComponentModel.DataAnnotations;

namespace StudentEvents.Api.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required, MaxLength(120)]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(200)]
        public string Email { get; set; } = string.Empty;

        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}
