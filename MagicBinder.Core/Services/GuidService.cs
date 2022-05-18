namespace MagicBinder.Core.Services;

public class GuidService
{
    public virtual Guid GetGuid() => Guid.NewGuid();
}