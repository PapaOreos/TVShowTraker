namespace TVShowTraker.Models.Auth
{
    public class AuthenticationResponse
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; }


        public AuthenticationResponse(User user, string token)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            Username = user.Username;
            Token = token;
        }
    }
}
