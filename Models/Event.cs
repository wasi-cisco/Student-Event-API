using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using StudentEvents.Api.Models;

namespace StudentEvents.Api.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(150)]
        public string Venue { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }  // event date/time (UTC)

        [MaxLength(1000)]
        public string? Description { get; set; }

        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}