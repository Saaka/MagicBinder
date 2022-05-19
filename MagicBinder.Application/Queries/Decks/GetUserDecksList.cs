using MagicBinder.Application.Mappers;
using MagicBinder.Application.Models.Decks;
using MagicBinder.Core.Models;
using MagicBinder.Core.Requests;
using MagicBinder.Infrastructure.Repositories;

namespace MagicBinder.Application.Queries.Decks;

public record GetUserDecksList : Request<PagedList<DeckInfoModel>>, IPageableRequest
{
    public string? Name { get; init; }
    public int PageSize { get; init; }
    public int PageNumber { get; init; }
}

public class GetUserDecksListHandler: RequestHandler<GetUserDecksList, PagedList<DeckInfoModel>>
{
    private readonly DecksRepository _decksRepository;
    private readonly IRequestContextService _requestContextService;

    public GetUserDecksListHandler(DecksRepository decksRepository, IRequestContextService requestContextService)
    {
        _decksRepository = decksRepository;
        _requestContextService = requestContextService;
    }
    
    public override async Task<RequestResult<PagedList<DeckInfoModel>>> Handle(GetUserDecksList request, CancellationToken cancellationToken)
    {
        if (!_requestContextService.CurrentContext.IsAuthorized) throw new UnauthorizedAccessException();

        var decks = await _decksRepository.GetAsync(request.MapToQueryParams(_requestContextService.CurrentContext.User.Id), cancellationToken);

        return request.Success(decks.MapToResponse(DeckMapper.MapToDeckInfo));
    }
}