namespace MagicBinder.Application.Models.Users;

public class UserModel
{
    public Guid UserGuid { get; set; }
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public string ImageUrl { get; set; }
    public bool IsAdmin { get; set; }
}