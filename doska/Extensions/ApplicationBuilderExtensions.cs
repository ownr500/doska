using doska.Configurations;
using doska.Core.Options;

namespace doska.Extensions;

internal static class ApplicationBuilderExtensions
{
    public static void RegisterConfigurationOptions(this WebApplicationBuilder builder)
    {
        builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
        builder.Services.ConfigureOptions<ConfigureJwtBearerOptions>();
        builder.Services.ConfigureOptions<ConfigureAuthenticationOptions>();
    }

    public static void RegisterOptions(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection(nameof(TokenOptions)));
    }
}