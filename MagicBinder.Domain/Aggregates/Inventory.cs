using MagicBinder.Domain.Aggregates.Entities;

namespace MagicBinder.Domain.Aggregates;

public class Inventory
{
    public Inventory(Guid oracleId, Guid userGuid) => (OracleId, UserGuid) = (oracleId, userGuid);

    private Inventory()
    {
    }

    public Guid OracleId { get; private set; }
    public Guid UserGuid { get; private set; }
    public int TotalCount { get; private set; }
    public List<UserCard> Cards { get; private set; } = new();

    public Inventory AddCard(UserCard card)
    {
        var cardPrinting = GetSameVersion(card);
        if (cardPrinting == null)
            Cards.Add(card);
        else
            cardPrinting.Count += card.Count;

        TotalCount = Cards.Sum(x => x.Count);
        return this;
    }

    private UserCard? GetSameVersion(UserCard card) => Cards.FirstOrDefault(x => x.CardId == card.CardId && x.IsFoil == card.IsFoil);
}