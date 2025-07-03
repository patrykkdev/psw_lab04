namespace EventRegistrationApp.Models
{
    public class EventRegistration
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public string ParticipationType { get; set; } = string.Empty;
        public string FoodPreference { get; set; } = string.Empty;
        public bool IsConfirmed { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
