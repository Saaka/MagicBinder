namespace MagicBinder.Application.Exceptions;

public abstract class NotFoundException : Exception
{
    public object Id { get; }
    public string Type { get; }

    protected NotFoundException(object id, string type) : base($"{type} {id} not found")
    {
        Id = id;
        Type = type;
    }
}

public class CardNotFoundException : NotFoundException
{
    public CardNotFoundException(Guid oracleId) : base(oracleId, "Card")
    {
    }
}

public class CardPrintingNotFoundException : NotFoundException
{
    public CardPrintingNotFoundException(Guid cardId) : base(cardId, "CardPrinting")
    {
    }
}