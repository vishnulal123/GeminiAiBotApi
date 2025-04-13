

namespace GeminiAiBotApi.Handler.Interface
{
    public interface IChatBotHandler
    {
        Task<string> GeminiAiBot(string query, string connectionId, Guid chatId, bool isTempChat = false, bool isUpdateName = false);
    }
}
