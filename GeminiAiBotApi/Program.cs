using GeminiAiBotApi.Handler;
using GeminiAiBotApi.Handler.Interface;
using GeminiAiBotApi.Hub;
using GeminiAiBotApi.Services;
using GeminiAiBotApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddTransient<IChatBotHandler, ChatBotHandler>();
builder.Services.AddSingleton<ICacheservice, Cacheservice>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapHub<NotificationHub>("/notificationHub");
app.MapControllers();

app.Run();
