namespace Yapily.Core.SDK
{
    public static class YapilyConfig
    {
        public static string AppKey { get; private set; } = string.Empty;
        public static string AppSecret { get; private set; } = string.Empty;

        public static readonly string BaseUrl = "https://api.yapily.com";

        public static void Initialize(string appKey, string appSecret)
        {
            AppKey = appKey;
            AppSecret = appSecret;
        }
    }
}
