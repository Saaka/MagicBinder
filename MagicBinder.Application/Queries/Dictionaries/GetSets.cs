using MagicBinder.Application.Mappers;
using MagicBinder.Application.Models.Dictionaries;
using MagicBinder.Core.Requests;
using MagicBinder.Infrastructure.Repositories;

namespace MagicBinder.Application.Queries.Dictionaries;

public record GetSets : Request<ICollection<SetModel>>;

public class GetCardsInfoQueryHandler : RequestHandler<GetSets, ICollection<SetModel>>
{
    private readonly SetsRepository _setsRepository;

    public GetCardsInfoQueryHandler(SetsRepository setsRepository)
    {
        _setsRepository = setsRepository;
    }

    public override async Task<RequestResult<ICollection<SetModel>>> Handle(GetSets request, CancellationToken cancellationToken)
    {
        var sets = await _setsRepository.GetSets(cancellationToken);

        return request.Success(sets.Select(SetsMapper.MapToModel).ToList());
    }
}