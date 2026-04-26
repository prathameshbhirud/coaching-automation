using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using CoachingAutomation.Models;

namespace CoachingAutomation.Services;

public class TelegramService : INotificationSender
{
    private readonly TelegramSettings _settings;
    private readonly HttpClient _httpClient;
    public NotificationChannel Channel => NotificationChannel.Telegram;

    public TelegramService(IOptions<TelegramSettings> settings)
    {
        _settings = settings.Value;
        _httpClient = new HttpClient();
    }

    public async Task SendMessage(string message)
    {
        var url = $"https://api.telegram.org/bot{_settings.BotToken}/sendMessage";

        var payload = new
        {
            chat_id = _settings.ChatId,
            text = message
        };

        var content = new StringContent(
            JsonSerializer.Serialize(payload),
            Encoding.UTF8,
            "application/json");

        await _httpClient.PostAsync(url, content);
    }

    public async Task SendAsync(string to, string message)
    {
        await SendMessage(message); // ignore 'to' or map later
    }
}