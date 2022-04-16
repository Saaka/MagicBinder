using MagicBinder.Application.Models.Cards;
using MagicBinder.Domain.Aggregates;
using MagicBinder.Domain.Aggregates.Entities;
using MagicBinder.Domain.Enums;
using MagicBinder.Infrastructure.Integrations.Scryfall;
using MagicBinder.Infrastructure.Integrations.Scryfall.Models;

namespace MagicBinder.Application.Mappers;

public static class CardMapper
{
    public static CardInfoModel MapToCardInfo(Card card) => new()
    {
        OracleId = card.OracleId,
        Name = card.Name,
        Image = card.LatestPrinting.CardImages?.Normal ?? string.Empty
    };

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
            Colors = model.Colors.MapToColors(),
            ColorIdentity = model.ColorIdentity.MapToColors(),
            Keywords = model.Keywords,
            Games = model.Games.MapToGames(),
            Layout = model.Layout.MapToLayout(),
            LegalIn = model.Legalities.MapToFormatLegality()
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