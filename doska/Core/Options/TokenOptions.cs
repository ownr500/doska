using doska.Core.Enums;

namespace doska.Core.Options;

public sealed class TokenOptions
{
    public Dictionary<TokenType, TokenInfo> TokenInfos { get; init; } = new();
}