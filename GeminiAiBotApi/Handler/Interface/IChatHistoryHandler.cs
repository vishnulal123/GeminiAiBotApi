using GeminiAiBotApi.Dtos;

namespace GeminiAiBotApi.Handler.Interface
{
    public interface IChatHistoryHandler
    {
        Task<List<ChatModel>> GetAllChatByConnectionId(string connectionId);
        Task<bool> UpdateChatName(string connectionId, Guid chatId);
    }
}
