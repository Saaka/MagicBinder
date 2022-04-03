namespace MagicBinder.Infrastructure.Configurations;

public record HangfireConfig
{
    public bool DashboardEnabled { get; init; }
    public string DatabaseName { get; init; }
    public int RetryCount { get; set; }
}