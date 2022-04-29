using MagicBinder.Application.Models.Inventories;
using MagicBinder.Core.Requests;
using MagicBinder.Infrastructure.Repositories;

namespace MagicBinder.Application.Queries.Inventories;

public record GetCardInventory(Guid OracleId) : Request<List<CardInventoryModel>>;

public class GetCardInventoryHandler : RequestHandler<GetCardInventory, List<CardInventoryModel>>
{
    private readonly InventoriesRepository _inventoriesRepository;

    public GetCardInventoryHandler(InventoriesRepository inventoriesRepository)
    {
        _inventoriesRepository = inventoriesRepository;
    }
    
    public override async Task<RequestResult<List<CardInventoryModel>>> Handle(GetCardInventory request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}