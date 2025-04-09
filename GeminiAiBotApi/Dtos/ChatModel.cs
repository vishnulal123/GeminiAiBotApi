namespace GeminiAiBotApi.Dtos
{
    public class ChatModel
    {
        public Guid ChatId { get; set; } = Guid.Empty;
        public string ChatName { get; set; } = null!;
        public List<ChatHistoryModel> Messages { get; set; } = new();
    }
}
