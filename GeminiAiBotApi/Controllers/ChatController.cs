using GeminiAiBotApi.Handler.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GeminiAiBotApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController(IChatHistoryHandler chatHistoryHandler) : ControllerBase
    {

        [HttpGet("GetAllChats")]
        public async Task<IActionResult> GetAllChats(string userId)
        {
            var data = await Task.Run(() => chatHistoryHandler.GetAllChatByConnectionId(userId));
            return Ok(data);
        }
    }
}
