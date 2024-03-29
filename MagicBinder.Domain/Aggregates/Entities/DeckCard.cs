﻿using MagicBinder.Domain.Enums;

namespace MagicBinder.Domain.Aggregates.Entities;

public class DeckCard
{
    public Guid OracleId { get; set; }
    public Guid CardId { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string TypeLine { get; set; } = string.Empty;
    public int Count { get; set; }
    public decimal Cmc { get; set; }
    public decimal CustomCmc { get; set; }
    public string ManaCost { get; set; } = string.Empty;
    public CardType CardType { get; set; }
    public ColorType[] Colors { get; set; } = Array.Empty<ColorType>();
    public ColorType[] ColorIdentity { get; set; } = Array.Empty<ColorType>();
}