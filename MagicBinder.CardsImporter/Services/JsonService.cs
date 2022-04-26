namespace MagicBinder.CardsImporter.Services;

public class JsonService
{
    public async Task<string> GetJson(string fileName)
    {
        using var streamReader = File.OpenText(fileName);
        return await streamReader.ReadToEndAsync();
    }
}