namespace GeminiAiBotApi.Hub
{
    using GeminiAiBotApi.Handler.Interface;
    using Microsoft.AspNetCore.SignalR;
    public class NotificationHub(IChatBotHandler chatBotHandler) : Hub
    {
        public async Task SendMessage(string user, string message, Guid chatId)
        {
            var result = await chatBotHandler.GeminiAiBot(message, user, chatId);
            await Clients.Caller.SendAsync("ReceiveMessage", "aiBot", result);
        }
    }
}
