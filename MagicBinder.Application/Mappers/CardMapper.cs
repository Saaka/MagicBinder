using MagicBinder.Application.Models.Cards;
using MagicBinder.Domain.Aggregates;
using MagicBinder.Domain.Aggregates.Entities;
using MagicBinder.Domain.Enums;
using MagicBinder.Infrastructure.Integrations.Scryfall;
using MagicBinder.Infrastructure.Integrations.Scryfall.Models;

namespace MagicBinder.Application.Mappers;

public static class CardMapper
{
    public static Card MapToCard(this CardModel model, Card? card = null)
    {
        card ??= new Card
        {
            OracleId = model.OracleId,
            Name = model.Name,
            Rarity = model.Rarity,
            Cmc = model.Cmc,
            ManaCost = model.ManaCost,
            TypeLine = model.TypeLine,
            OracleText = model.OracleText,
            Power = model.Power,
            Toughness = model.Toughness,
            Colors = model.Colors,
            ColorIdentity = model.ColorIdentity,
            Keywords = model.Keywords,
            Games = model.Games,
            Layout = MapToLayout(model.Layout),
            LegalIn = MapToFormatLegality(model.Legalities)
        };

        return card;
    }

    public static CardPrinting MapToCardPrinting(this CardModel model, CardPrinting? printing = null)
    {
        printing ??= new CardPrinting
        {
            CardId = model.CardId,
            OracleId = model.OracleId,
            Name = model.Name,
            ReleasedAt = model.ReleasedAt,
            ScryfallUri = model.ScryfallUri,
            OracleText = model.OracleText,
            SetId = model.SetId,
            Set = model.Set,
            SetName = model.SetName,
            CollectorNumber = model.CollectorNumber,
            FlavorText = model.FlavorText,
            Artist = model.Artist,
            Lang = model.Lang,
            CardImages = model.ImageUris.MapToCardImages()
        };

        return printing;
    }

    public static CardImages? MapToCardImages(this ImageUrisModel? imageUris) => imageUris == null
        ? null
        : new CardImages
        {
            Small = imageUris.Small,
            Normal = imageUris.Normal,
            Large = imageUris.Large,
            Art = imageUris.Art,
            PngRounded = imageUris.PngRounded
        };

    public static CardInfoModel MapToCardInfo(Card card) => new()
    {
        OracleId = card.OracleId,
        Name = card.Name,
        Image = card.LatestPrinting.CardImages?.Normal ?? string.Empty
    };

    public static LayoutType MapToLayout(string layoutType) =>
        layoutType switch
        {
            ScryfallConstants.Layouts.Adventure => LayoutType.Adventure,
            ScryfallConstants.Layouts.Class => LayoutType.Class,
            ScryfallConstants.Layouts.Flip => LayoutType.Flip,
            ScryfallConstants.Layouts.Leveler => LayoutType.Leveler,
            ScryfallConstants.Layouts.Mdfc => LayoutType.Mdfc,
            ScryfallConstants.Layouts.Meld => LayoutType.Meld,
            ScryfallConstants.Layouts.Normal => LayoutType.Normal,
            ScryfallConstants.Layouts.Saga => LayoutType.Saga,
            ScryfallConstants.Layouts.Split => LayoutType.Split,
            ScryfallConstants.Layouts.Transform => LayoutType.Transform,
            _ => LayoutType.Other
        };

    private static FormatType[] MapToFormatLegality(LegalitiesModel legalities)
    {
        var formatLegality = new List<FormatType>();
        if (legalities.Commander == LegalitiesModel.Legal) formatLegality.Add(FormatType.Commander);
        if (legalities.Standard == LegalitiesModel.Legal) formatLegality.Add(FormatType.Standard);
        if (legalities.Modern == LegalitiesModel.Legal) formatLegality.Add(FormatType.Modern);
        if (legalities.Pauper == LegalitiesModel.Legal) formatLegality.Add(FormatType.Pauper);
        if (legalities.Pioneer == LegalitiesModel.Legal) formatLegality.Add(FormatType.Pioneer);

        return formatLegality.ToArray();
    }
}