namespace Shop.Webapp.Application.Auth.Models
{
    public class RegisterModel
    {
        public string? SurName { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Password { get; set; } = string.Empty;
        public string? ConfirmPassword { get; set; } = string.Empty;
    }
}
