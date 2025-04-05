namespace GeminiAiBotApi.Dtos
{
    public class ChatMessageModel
    {
        public Guid ChatId { get; set; }
        public string ChatName { get; set; } = null!;
        public List<Object> Message { get; set; } = [];
    }
}
