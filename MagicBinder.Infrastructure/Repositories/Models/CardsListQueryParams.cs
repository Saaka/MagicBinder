using MagicBinder.Core.Models;

namespace MagicBinder.Infrastructure.Repositories.Models;

public record CardsListQueryParams : IPageableRequest
{
    public string? Name { get; set; }
    public string? TypeLine { get; set; }
    public string? OracleText { get; set; }
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}