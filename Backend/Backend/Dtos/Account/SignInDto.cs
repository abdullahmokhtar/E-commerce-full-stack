namespace Backend.API.Dtos.Account
{
    public class SignInDto
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Length(8, 16)]
        public string Password { get; set; } = string.Empty;
    }
}
