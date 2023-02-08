using MagicBinder.Application.Mappers;
using MagicBinder.Application.Models.Inventories;
using MagicBinder.Core.Models;
using MagicBinder.Core.Requests;
using MagicBinder.Infrastructure.Repositories;

namespace MagicBinder.Application.Queries.Inventories;

public record GetUserInventory : Request<PagedList<UserInventoryModel>>, IPageableRequest
{
    public string? CardName { get; init; }
    public string? SetName { get; init; }
    public int PageSize { get; init; }
    public int PageNumber { get; init; }
}

public class GetUserInventoryHandler : RequestHandler<GetUserInventory, PagedList<UserInventoryModel>>
{
    private readonly InventoriesRepository _inventoriesRepository;
    private readonly IRequestContextService _requestContextService;

    public GetUserInventoryHandler(InventoriesRepository inventoriesRepository, IRequestContextService requestContextService)
    {
        _inventoriesRepository = inventoriesRepository;
        _requestContextService = requestContextService;
    }

    public override async Task<RequestResult<PagedList<UserInventoryModel>>> Handle(GetUserInventory request, CancellationToken cancellationToken)
    {
        if (!_requestContextService.CurrentContext.IsAuthorized) throw new UnauthorizedAccessException();

        var inventory = await _inventoriesRepository
            .GetUserInventoryAsync(request.MapToQueryParams(_requestContextService.CurrentContext.User.Id), cancellationToken);
        return request.Success(inventory.MapToResponse(InventoryMapper.MapToUserInventory));
    }
}