namespace Backend.API.Dtos.Account
{
    public class ForgetPasswordDto
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
