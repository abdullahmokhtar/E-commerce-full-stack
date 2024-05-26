namespace Backend.API.Dtos.Account
{
    public class SignUpDto
    {
        public string Name { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [RegularExpression("^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{8,16}$", ErrorMessage = "Password must contain at least 1 uppercase, 1 lowercase, 1 special character, 1 digit and length 8 characters")]
        public string Password { get; set; } = string.Empty;
        [Compare("Password")]
        public string RePassword { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
