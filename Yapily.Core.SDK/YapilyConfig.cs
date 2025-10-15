namespace Yapily.Core.SDK
{
    public static class YapilyConfig
    {
        public static string AppKey { get; private set; } = string.Empty;
        public static string AppSecret { get; private set; } = string.Empty;

        public static string BaseUrl { get; private set; } = string.Empty;
        public static string CallBackURL { get; private set; } = string.Empty;

        public static void Initialize(string appKey, string appSecret, string baseURL, string callBackURL)
        {
            AppKey = appKey;
            AppSecret = appSecret;
            BaseUrl = baseURL;
            CallBackURL = callBackURL;
        }
    }
}
