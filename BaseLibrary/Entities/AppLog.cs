using BaseLibrary.Extentions;

namespace BaseLibrary.Entities
{
    public class AppLog
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Timestamp { get; set; }
        public LogAction LogAction { get; set; }
        public required string Details { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }

        public virtual required AppUser User { get; set; }
    }
}
