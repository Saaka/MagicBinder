using MagicBinder.Core.Models;

namespace MagicBinder.Infrastructure.Repositories.Models;

public class UserInventoryQueryParams : IPageableRequest
{
    public Guid UserId { get; set; }
    public string? CardName { get; set; }
    public string? SetName { get; set; }
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}