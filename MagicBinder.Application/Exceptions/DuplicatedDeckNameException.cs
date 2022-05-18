namespace MagicBinder.Application.Exceptions;

public class DuplicatedDeckNameException : ArgumentException
{
    public string Name { get; }

    public DuplicatedDeckNameException(string name) : base("Deck name {Name} is already in use", name)
    {
        Name = name;
    }
}