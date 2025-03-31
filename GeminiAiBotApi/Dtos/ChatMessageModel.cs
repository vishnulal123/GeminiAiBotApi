namespace GeminiAiBotApi.Dtos
{
    public class ChatMessageModel
    {
        public Guid ChatId { get; set; }
        public List<Object> Message { get; set; } = [];
    }
}
