namespace MagicBinder.CardsImporter.Services;

public class CardsJsonService
{
    public async Task<string> GetJson(string fileName)
    {
        using var streamReader = File.OpenText(fileName);
        return await streamReader.ReadToEndAsync();
    }
}