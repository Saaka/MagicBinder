using MagicBinder.Core.Models;

namespace MagicBinder.Infrastructure.Repositories.Models;

public record UserDecksQueryParams : IPageableRequest
{
    public Guid OwnerId { get; set; }
    public string? Name { get; set; }
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}