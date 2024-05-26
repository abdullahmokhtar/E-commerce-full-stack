namespace Backend.DAL.Entities
{
    public class UserResetCode
    {
        public AppUser AppUser { get; set; } = null!;
        public string AppUserId { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public bool IsCodeVerified { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
