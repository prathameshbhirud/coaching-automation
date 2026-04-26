using CoachingAutomation.Services;

namespace CoachingAutomation.Jobs;

public class NotificationJob
{
    private readonly GoogleSheetService _sheetService;
    private readonly NotificationService _notificationService;
    private readonly WhatsAppService _whatsAppService;
    private readonly TelegramService _telegramService;

    public NotificationJob(
        GoogleSheetService sheetService,
        NotificationService notificationService,
        WhatsAppService whatsAppService,
        TelegramService telegramService)
    {
        _sheetService = sheetService;
        _notificationService = notificationService;
        _whatsAppService = whatsAppService;
        _telegramService = telegramService;
    }

    public async Task Run()
    {
        var students = await _sheetService.GetStudentsAsync();
        var messages = _notificationService.GenerateMessages(students);

        foreach (var msg in messages)
        {
            _whatsAppService.SendMessage(msg.Phone, msg.Message);
            await _telegramService.SendMessage(msg.Message);
        }
    }
}