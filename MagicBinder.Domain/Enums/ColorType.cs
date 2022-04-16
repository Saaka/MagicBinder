namespace MagicBinder.Domain.Enums;

[Flags]
public enum ColorType
{
    Colorless = 0,
    White = 1 << 0,
    Blue = 1 << 1,
    Black = 1 << 2,
    Red = 1 << 3,
    Green = 1 << 4,
    
    Azorius = White | Blue,
    Orzhov = White | Black,
    Boros = White | Red,
    Selesnya = White | Green,
    Dimir = Blue | Black,
    Izzet = Blue | Red,
    Simic = Blue | Green,
    Rakdos = Black | Red,
    Golgari = Black | Green,
    Gruul = Red | Green,
    
    Esper = White | Blue | Black,
    Jeskai = White | Blue | Red,
    Bant = White | Blue | Green,
    Mardu = White | Black | Red,
    Abzan = White | Black | Green,
    Naya = White | Red | Green,
    Grixis = Blue | Black | Red,
    Sultai = Blue | Black | Green,
    Temur = Blue | Red | Green,
    Jund = Black | Red | Green,

    Yore = White | Blue | Black | Red,
    Witch = White | Blue | Black | Green,
    Ink = White | Blue | Red | Green,
    Dune = White | Black | Red | Green,
    Glint = Blue | Black | Red | Green,
    
    Wubrg = White | Blue | Black | Red | Green
}