using doska.Configurations;

namespace doska.Extensions;

internal static class ApplicationBuilderExtensions
{
    public static void RegisterConfigurationOptions(this WebApplicationBuilder builder)
    {
        builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
    }
}