using System.ComponentModel;
using CoachingAutomation.Services;

namespace CoachingAutomation.Jobs;

public class NotificationJob
{
    private readonly GoogleSheetService _sheetService;
    private readonly NotificationService _notificationService;
    // private readonly WhatsAppService _whatsAppService;
    // private readonly TelegramService _telegramService;
    private readonly NotificationDispatcher _dispatcher;

    public NotificationJob(
        GoogleSheetService sheetService,
        NotificationService notificationService,
        NotificationDispatcher dispatcher)
    {
        _sheetService = sheetService;
        _notificationService = notificationService;
        _dispatcher = dispatcher;
    }

    public async Task Run()
    {
        var students = await _sheetService.GetStudentsAsync();
        var messages = _notificationService.GenerateMessages(students);

        /*
        foreach (var msg in messages)
        {
            // _whatsAppService.SendMessage(msg.Phone, msg.Message);
            // await _telegramService.SendMessage(msg.Message);
            await _dispatcher.SendAsync(
                student.PreferredChannel,
                student.ParentPhone,
                msg.Message
            );
        }
        */

        foreach (var student in students)
        {
            foreach (var msg in messages)
            {
                // _whatsAppService.SendMessage(msg.Phone, msg.Message);
                // await _telegramService.SendMessage(msg.Message);
                await _dispatcher.SendAsync(
                    student.PreferredChannel,
                    student.ParentPhone,
                    msg.Message
                );
            }
        }
    }
}