﻿namespace MagicBinder.Domain.Aggregates;

public class User
{
    public Guid UserGuid { get; private set; }
    public string DisplayName { get; private set; }
    public string Email { get; private set; }
    public string ImageUrl { get; private set; }
    public bool IsAdmin { get; private set; }

    private User()
    {
    }

    public User(Guid userGuid, string displayName, string email, string imageUrl, bool isAdmin = false)
    {
        UserGuid = userGuid;
        DisplayName = displayName;
        Email = email;
        ImageUrl = imageUrl;
        IsAdmin = isAdmin;
    }

    public User SetAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public User SetImageUrl(string imageUrl)
    {
        ImageUrl = imageUrl;
        return this;
    }
}