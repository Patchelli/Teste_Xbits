namespace Teste_Xbits.Domain.Providers;

public sealed class JwtOptions
{
    public const string SectionName = "Jwt";
    public required string JwtKey { get; init; }
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required double DurationInMinutes { get; init; }
    public required bool RequireHttpsMetadata { get; init; }
}