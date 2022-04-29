using MagicBinder.Application.Mappers;
using MagicBinder.Application.Models.Inventories;
using MagicBinder.Core.Requests;
using MagicBinder.Infrastructure.Repositories;

namespace MagicBinder.Application.Queries.Inventories;

public record GetCardInventory(Guid OracleId) : Request<CardInventoryModel>;

public class GetCardInventoryHandler : RequestHandler<GetCardInventory, CardInventoryModel>
{
    private readonly InventoriesRepository _inventoriesRepository;
    private readonly IRequestContextService _requestContextService;

    public GetCardInventoryHandler(InventoriesRepository inventoriesRepository, IRequestContextService requestContextService)
    {
        _inventoriesRepository = inventoriesRepository;
        _requestContextService = requestContextService;
    }

    public override async Task<RequestResult<CardInventoryModel>> Handle(GetCardInventory request, CancellationToken cancellationToken)
    {
        if (!_requestContextService.CurrentContext.IsAuthorized) throw new UnauthorizedAccessException();
        
        var inventory = await _inventoriesRepository.GetAsync(request.OracleId, _requestContextService.CurrentContext.User.Id, cancellationToken);
        return request.Success(inventory.MapToModel(request.OracleId));
    }
}