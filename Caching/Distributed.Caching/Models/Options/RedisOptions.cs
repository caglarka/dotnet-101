namespace Distributed.Caching.Models.Options;

public class RedisOptions
{
    public List<Host> Hosts { get; init; } = [];
    public string Password { get; set; } = string.Empty;
    public bool Ssl { get; init; } = false;
    public int Database { get; init; } = 0;
    public int ConnectTimeout { get; init; } = 5_000;
    public int ConnectRetry { get; init; } = 3;
    public int TtlMinutes { get; set; } = 3;

    public sealed record Host
    {
        public string Hostname { get; init; } = default!;
        public int Port { get; init; } = 6379;
    }
}