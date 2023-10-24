namespace Shop.Webapp.Application.Auth.Models
{
    public class TokenResult
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
