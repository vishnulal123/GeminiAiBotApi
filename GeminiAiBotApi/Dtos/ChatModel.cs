namespace GeminiAiBotApi.Dtos
{
    public class ChatModel
    {

        public string ChatName { get; set; } = null!;
        public List<ChatHistoryModel> Messages { get; set; } = new();


    }
}
