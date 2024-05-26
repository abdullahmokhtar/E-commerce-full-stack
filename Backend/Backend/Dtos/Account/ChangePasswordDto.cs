namespace Backend.API.Dtos.Account
{
    public class ChangePasswordDto
    {
        public string CurrentPassword { get; set; } = string.Empty;
        [RegularExpression("^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{8,16}$", ErrorMessage = "Password must contain at least 1 uppercase, 1 lowercase, 1 special character, 1 digit and length 8 characters")]
        public string Password { get; set; } = string.Empty;
        [Compare("Password")]
        public string RePassword { get; set; } = string.Empty;
    }
}
