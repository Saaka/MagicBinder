using MagicBinder.Application.Models.Cards;
using MagicBinder.Domain.Aggregates;
using MagicBinder.Domain.Aggregates.Entities;

namespace MagicBinder.Application.Mappers;

public static class CardMapper
{
    public static CardInfoModel MapToCardInfo(Card card) => new()
    {
        OracleId = card.OracleId,
        Name = card.Name,
        TypeLine = card.LatestPrinting.TypeLine,
        Image = card.LatestPrinting.CardImages?.Normal ?? string.Empty,
        Artist = card.LatestPrinting.Artist
    };

    public static CardDetailsModel? MapToCardDetails(this Card? card) => card == null
        ? null
        : new CardDetailsModel
        {
            OracleId = card.OracleId,
            Name = card.Name,
            TypeLine = card.LatestPrinting.TypeLine,
            Cmc = card.Cmc,
            ManaCost = card.ManaCost,
            OracleText = card.LatestPrinting.OracleText,
            ScryfallUri = card.LatestPrinting.ScryfallUri,
            Colors = card.Colors,
            Images = card.LatestPrinting.CardImages.MapToImages(),
            Set = card.LatestPrinting.Set,
            SetName = card.LatestPrinting.SetName,
            CollectorNumber = card.LatestPrinting.CollectorNumber,
            Printings = card.CardPrintings.Select(MapToPrintingDetails).ToList()
        };

    private static CardDetailsModel.CardPrintingDetailsModel MapToPrintingDetails(this CardPrinting printing) => new()
    {
        CardId = printing.CardId,
        SetName = printing.SetName,
        Image = printing.CardImages?.Normal ?? string.Empty,
        CollectorNumber = printing.CollectorNumber
    };

    private static CardImagesModel MapToImages(this CardImages? images) => images == null
        ? new CardImagesModel()
        : new CardImagesModel
        {
            Normal = images.Normal,
            Large = images.Large,
            Art = images.Art,
            PngRounded = images.PngRounded
        };
}