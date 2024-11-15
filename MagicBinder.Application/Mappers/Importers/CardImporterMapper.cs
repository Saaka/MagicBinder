using MagicBinder.Domain.Aggregates;
using MagicBinder.Domain.Aggregates.Entities;
using MagicBinder.Domain.Enums;
using MagicBinder.Infrastructure.Integrations.Scryfall;
using MagicBinder.Infrastructure.Integrations.Scryfall.Models;

namespace MagicBinder.Application.Mappers.Importers;

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
            LayoutType.DoubleFacedToken => card.MapDoubleFacedTokenFields(),
            _ => card
        };

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

    public static Card MapMeldCardFields(this Card card)
    {
        //TODO MELD
        return card;
    }

    private static Card MapDoubleFacedTokenFields(this Card card)
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
            Cmc = model.Cmc,
            ManaCost = model.ManaCost,
            Power = model.Power,
            Toughness = model.Toughness,
            Colors = model.Colors.MapToColors(),
            ColorIdentity = model.ColorIdentity.MapToColors(),
            ProducedMana = model.ProducedMana.MapToColors(),
            Keywords = model.Keywords,
            Games = model.Games.MapToGames(),
            Layout = model.Layout.MapToLayout(),
            LegalIn = model.Legalities.MapToFormatLegality(),
            Loyalty = model.Loyalty,
            CardType = model.TypeLine.MapToCardType()
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
            Rarity = model.Rarity,
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
            CardFaces = model.CardFaces.Select(MapToCardFace).ToList(),
            AllParts = model.AllParts.Select(MapToCardPart).ToList()
        };

        return printing;
    }

    private static CardPart MapToCardPart(this CardPartModel part) => new()
    {
        CardId = part.CardId,
        OracleId = part.CardId,
        Name = part.Name,
        TypeLine = part.TypeLine,
        Component = part.Component.MapToPartComponent()
    };

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
            ScryfallCardsConstants.Layouts.Adventure => LayoutType.Adventure,
            ScryfallCardsConstants.Layouts.Class => LayoutType.Class,
            ScryfallCardsConstants.Layouts.Flip => LayoutType.Flip,
            ScryfallCardsConstants.Layouts.Leveler => LayoutType.Leveler,
            ScryfallCardsConstants.Layouts.Mdfc => LayoutType.Mdfc,
            ScryfallCardsConstants.Layouts.Meld => LayoutType.Meld,
            ScryfallCardsConstants.Layouts.Normal => LayoutType.Normal,
            ScryfallCardsConstants.Layouts.Saga => LayoutType.Saga,
            ScryfallCardsConstants.Layouts.Split => LayoutType.Split,
            ScryfallCardsConstants.Layouts.Transform => LayoutType.Transform,
            ScryfallCardsConstants.Layouts.Token => LayoutType.Token,
            ScryfallCardsConstants.Layouts.DoubleFacedToken => LayoutType.DoubleFacedToken,
            ScryfallCardsConstants.Layouts.Planar => LayoutType.Planar,
            ScryfallCardsConstants.Layouts.Scheme => LayoutType.Scheme,
            ScryfallCardsConstants.Layouts.Vanguard => LayoutType.Vanguard,
            ScryfallCardsConstants.Layouts.Emblem => LayoutType.Emblem,
            ScryfallCardsConstants.Layouts.Augment => LayoutType.Augment,
            ScryfallCardsConstants.Layouts.Host => LayoutType.Host,
            ScryfallCardsConstants.Layouts.Reversible => LayoutType.Reversible,
            ScryfallCardsConstants.Layouts.Battle => LayoutType.Battle,
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
            ScryfallCardsConstants.Colors.White => ColorType.White,
            ScryfallCardsConstants.Colors.Blue => ColorType.Blue,
            ScryfallCardsConstants.Colors.Black => ColorType.Black,
            ScryfallCardsConstants.Colors.Red => ColorType.Red,
            ScryfallCardsConstants.Colors.Green => ColorType.Green,
            _ => ColorType.Colorless
        };

    private static CardType MapToCardType(this string typeLine) =>
        string.IsNullOrWhiteSpace(typeLine)
            ? CardType.Other
            : typeLine.ToLower().Split("//")[0] switch
            {
                { } @type when @type.ContainsType(CardType.Creature) => CardType.Creature,
                { } @type when @type.ContainsType(CardType.Artifact) => CardType.Artifact,
                { } @type when @type.ContainsType(CardType.Enchantment) => CardType.Enchantment,
                { } @type when @type.ContainsType(CardType.Planeswalker) => CardType.Planeswalker,
                { } @type when @type.ContainsType(CardType.Instant) => CardType.Instant,
                { } @type when @type.ContainsType(CardType.Sorcery) => CardType.Sorcery,
                { } @type when @type.ContainsType(CardType.Land) => CardType.Land,
                { } @type when @type.ContainsType(CardType.Conspiracy) => CardType.Conspiracy,
                { } @type when @type.ContainsType(CardType.Battle) => CardType.Battle,
                { } @type when @type.ContainsType(CardType.Dungeon) => CardType.Dungeon,
                { } @type when @type.ContainsType(CardType.Emblem) => CardType.Emblem,
                { } @type when @type.ContainsType(CardType.Hero) => CardType.Hero,
                { } @type when @type.ContainsType(CardType.Phenomenon) => CardType.Phenomenon,
                { } @type when @type.ContainsType(CardType.Plane) => CardType.Plane,
                { } @type when @type.ContainsType(CardType.Scheme) => CardType.Scheme,
                { } @type when @type.ContainsType(CardType.Vanguard) => CardType.Vanguard,
                _ => CardType.Other
            };

    private static bool ContainsType(this string typeLine, CardType cardType) => typeLine.Contains(cardType.ToString().ToLower());

    private static PartComponentType MapToPartComponent(this string component) =>
        component switch
        {
            ScryfallCardsConstants.RelatedComponent.Token => PartComponentType.Token,
            ScryfallCardsConstants.RelatedComponent.MeldPart => PartComponentType.MeldPart,
            ScryfallCardsConstants.RelatedComponent.MeldResult => PartComponentType.MeldResult,
            ScryfallCardsConstants.RelatedComponent.ComboPiece => PartComponentType.ComboPiece,
            _ => PartComponentType.Other
        };
}