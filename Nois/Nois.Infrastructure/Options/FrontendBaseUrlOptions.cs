namespace Nois.Infrastructure.Options
{
    public class FrontendBaseUrlOptions
    {
        // This key MUST match the section name in your appsettings.json
        public const string FrontendBaseUrl = "Frontend";

        // This property will hold the value of "BaseUrl" from the config file
        public string BaseUrl { get; set; } = string.Empty;
    }
}
