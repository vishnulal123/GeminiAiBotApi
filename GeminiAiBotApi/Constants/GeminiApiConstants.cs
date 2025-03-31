namespace GeminiAiBotApi.Constants
{
    public class GeminiApiConstants
    {
        public static readonly string GeminiApiBaseUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent";
        public static readonly int Temperature = 1;
        public static readonly int TopK = 40;
        public static readonly double TopP = 0.95;
        public static readonly int MaxOutputTokens = 100;
        public static readonly string ResponseMimeType = "text/plain";
    }
}
