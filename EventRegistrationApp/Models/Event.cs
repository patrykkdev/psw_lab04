using System;

namespace EventRegistrationApp.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Agenda { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public bool IsActive { get; set; }
    }
}