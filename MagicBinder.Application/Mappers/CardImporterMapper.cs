using MagicBinder.Domain.Aggregates;
using MagicBinder.Domain.Aggregates.Entities;
using MagicBinder.Domain.Enums;
using MagicBinder.Infrastructure.Integrations.Scryfall;
using MagicBinder.Infrastructure.Integrations.Scryfall.Models;

namespace MagicBinder.Application.Mappers;

public static class CardImporterMapper
{
    public static Card MapMissingFields(this Card card) =>
        card.Layout switch
        {
            LayoutType.Adventure => card.MapAdventureCardFields(),
            LayoutType.Split => card.MapSplitCardFields(),
            LayoutType.Flip => card.MapFlipCardFields(),
            LayoutType.Transform => card.MapTransformCardFields(),
            LayoutType.Mdfc => card.MapMdfcCardFields(),
            LayoutType.Meld => card.MapMeldCardFields(),
            _ => card
        };

    private static Card MapAdventureCardFields(this Card card)
    {
        card.CardPrintings.ForEach(x => x.OracleText = x.CardFaces.GetOracleText());
        return card;
    }

    private static Card MapSplitCardFields(this Card card)
    {
        card.CardPrintings.ForEach(x => x.OracleText = x.CardFaces.GetOracleText());
        return card;
    }

    private static Card MapFlipCardFields(this Card card)
    {
        card.CardPrintings.ForEach(x => x.OracleText = x.CardFaces.GetOracleText());
        return card;
    }

    private static Card MapTransformCardFields(this Card card)
    {
        var latestPrinting = card.LatestPrinting;
        var frontFace = latestPrinting.CardFaces.First();

        card.ManaCost = latestPrinting.CardFaces.GetManaCost();
        card.Power = frontFace.Power;
        card.Toughness = frontFace.Toughness;
        card.Loyalty = frontFace.Loyalty;
        card.Colors = frontFace.Colors;

        card.CardPrintings.ForEach(x =>
        {
            var printingFrontFace = x.CardFaces.First();
            x.OracleText = x.CardFaces.GetOracleText();
            x.CardImages = printingFrontFace.CardImages;
            x.FlavorText = printingFrontFace.FlavorText;
        });

        return card;
    }

    private static Card MapMdfcCardFields(this Card card)
    {
        var latestPrinting = card.LatestPrinting;
        var frontFace = latestPrinting.CardFaces.First();

        card.ManaCost = latestPrinting.CardFaces.GetManaCost();
        card.Power = frontFace.Power;
        card.Toughness = frontFace.Toughness;
        card.Loyalty = frontFace.Loyalty;
        card.Colors = frontFace.Colors;

        card.CardPrintings.ForEach(x =>
        {
            var printingFrontFace = x.CardFaces.First();
            x.OracleText = x.CardFaces.GetOracleText();
            x.CardImages = printingFrontFace.CardImages;
            x.FlavorText = printingFrontFace.FlavorText;
        });

        return card;
    }

    public static Card MapMeldCardFields(this Card card)
    {
        return card;
    }

    public static string GetOracleText(this ICollection<CardFace> cardFaces)
    {
        var oracleTexts = cardFaces
            .Where(x => !string.IsNullOrEmpty(x.OracleText))
            .Select(x => x.OracleText)
            .ToList();

        if (!oracleTexts.Any()) return string.Empty;
        return oracleTexts.Count == 1
            ? oracleTexts.First()
            : string.Join($"{Environment.NewLine}//{Environment.NewLine}", oracleTexts);
    }

    public static string GetManaCost(this ICollection<CardFace> cardFaces)
    {
        var manaCosts = cardFaces
            .Where(x => !string.IsNullOrEmpty(x.ManaCost))
            .Select(x => x.ManaCost)
            .ToList();
        if (!manaCosts.Any()) return string.Empty;
        return manaCosts.Count == 1
            ? manaCosts.First()
            : string.Join(" // ", manaCosts);
    }

    public static Card MapToCard(this CardModel model, Card? card = null)
    {
        card ??= new Card
        {
            OracleId = model.OracleId,
            Name = model.Name,
            Rarity = model.Rarity,
            Cmc = model.Cmc,
            ManaCost = model.ManaCost,
            Power = model.Power,
            Toughness = model.Toughness,
            Colors = model.Colors.MapToColors(),
            ColorIdentity = model.ColorIdentity.MapToColors(),
            Keywords = model.Keywords,
            Games = model.Games.MapToGames(),
            Layout = model.Layout.MapToLayout(),
            LegalIn = model.Legalities.MapToFormatLegality(),
            Loyalty = model.Loyalty
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
            TypeLine = model.TypeLine,
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
            CardImages = model.ImageUris.MapToCardImages(),
            Games = model.Games.MapToGames(),
            LegalIn = model.Legalities.MapToFormatLegality(),
            CardFaces = model.CardFaces.Select(MapToCardFace).ToList()
        };

        return printing;
    }

    private static CardFace MapToCardFace(CardFaceModel face) => new()
    {
        Name = face.Name,
        Artist = face.Artist,
        Cmc = face.Cmc,
        Colors = face.Colors.MapToColors(),
        Layout = face.Layout.MapToLayout(LayoutType.Normal),
        Loyalty = face.Loyalty,
        Power = face.Power,
        Toughness = face.Toughness,
        CardImages = face.ImageUris.MapToCardImages(),
        FlavorText = face.FlavorText,
        ManaCost = face.ManaCost,
        OracleText = face.OracleText,
        TypeLine = face.TypeLine
    };

    private static CardImages? MapToCardImages(this ImageUrisModel? imageUris) => imageUris == null
        ? null
        : new CardImages
        {
            Small = imageUris.Small,
            Normal = imageUris.Normal,
            Large = imageUris.Large,
            Art = imageUris.Art,
            PngRounded = imageUris.PngRounded
        };

    private static LayoutType MapToLayout(this string layoutType, LayoutType defaultLayout = LayoutType.Other) =>
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
            _ => defaultLayout
        };

    private static FormatType[] MapToFormatLegality(this LegalitiesModel legalities)
    {
        var formatLegality = new List<FormatType>();
        if (legalities.Commander == LegalitiesModel.Legal) formatLegality.Add(FormatType.Commander);
        if (legalities.Standard == LegalitiesModel.Legal) formatLegality.Add(FormatType.Standard);
        if (legalities.Modern == LegalitiesModel.Legal) formatLegality.Add(FormatType.Modern);
        if (legalities.Pauper == LegalitiesModel.Legal) formatLegality.Add(FormatType.Pauper);
        if (legalities.Pioneer == LegalitiesModel.Legal) formatLegality.Add(FormatType.Pioneer);

        return formatLegality.ToArray();
    }

    private static GameType[] MapToGames(this string[] games)
        => games.Select(x => x.MapToGameType()).ToArray();

    private static GameType MapToGameType(this string gameType) =>
        Enum.TryParse(gameType, true, out GameType gameEnum) ? gameEnum : GameType.Unknown;

    private static ColorType[] MapToColors(this string[] modelColors)
        => !modelColors.Any()
            ? Array.Empty<ColorType>()
            : modelColors.Select(MapToColor).ToArray();


    private static ColorType MapToColor(string color) =>
        color switch
        {
            ScryfallConstants.Colors.White => ColorType.White,
            ScryfallConstants.Colors.Blue => ColorType.Blue,
            ScryfallConstants.Colors.Black => ColorType.Black,
            ScryfallConstants.Colors.Red => ColorType.Red,
            ScryfallConstants.Colors.Green => ColorType.Green,
            _ => ColorType.Colorless
        };
}