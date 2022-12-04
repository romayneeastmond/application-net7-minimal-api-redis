public static class BuilderHelper
{
    public static string GetConfigurationString(string key, WebApplicationBuilder builder)
    {
        if (builder == null || (builder != null && builder.Configuration == null))
        {
            return string.Empty;
        }

        if (builder != null && builder.Configuration != null && builder.Configuration[key] != null)
        {
            return builder.Configuration[key]!.ToString();
        }

        return string.Empty;
    }
}