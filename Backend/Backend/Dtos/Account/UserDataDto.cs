namespace Backend.API.Dtos.Account
{
    public class UserDataDto
    {
        [MaxLength(20)]
        public string UserName { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Length(11,11)]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
