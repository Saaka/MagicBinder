using MagicBinder.Domain.Aggregates;
using MagicBinder.Domain.Aggregates.Entities;
using MagicBinder.Infrastructure.Integrations.Scryfall.Models;

namespace MagicBinder.Application.Mappers;

public static class CardMapper
{
    public static Card MapToCard(this CardModel cardModel, Card? card = null)
    {
        card ??= new Card();
        card.CardId = cardModel.CardId;
        card.OracleId = cardModel.OracleId;
        card.Name = cardModel.Name;
        card.CardImages = cardModel.ImageUris.MapToCardImages();

        return card;
    }

    public static CardImages? MapToCardImages(this ImageUrisModel? imageUris) => imageUris == null ? null :  new CardImages
    {
        Small = imageUris.Small,
        Normal = imageUris.Normal,
        Large = imageUris.Large,
        Art = imageUris.Art,
        PngRounded = imageUris.PngRounded
    };
}