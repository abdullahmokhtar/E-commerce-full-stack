namespace Backend.API.Dtos.Account
{
    public class ResetPasswordDto
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [RegularExpression("^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{8,16}$", ErrorMessage = "Password must contain at least 1 uppercase, 1 lowercase, 1 special character, 1 digit and length 8 characters")]
        public string Password { get; set; } = string.Empty;
        [Compare("Password", ErrorMessage = "Password and Re Password does not match")]
        public string RePassword { get; set; } = string.Empty;
    }
}
