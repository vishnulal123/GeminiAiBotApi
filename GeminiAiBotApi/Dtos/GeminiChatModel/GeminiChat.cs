namespace GeminiAiBotApi.Dtos.GeminiChatModel
{
    public class GeminiChat
    {
        public class ChatMessage
        {
            public string Role { get; set; } = null!;
            public List<Part> Parts { get; set; } = null!;
        }

        public class Part
        {
            public string Text { get; set; } = null!;
        }
    }
}
