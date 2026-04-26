using CoachingAutomation.Models;

public class NotificationDispatcher
{
    private readonly IEnumerable<INotificationSender> _senders;

    public NotificationDispatcher(IEnumerable<INotificationSender> senders)
    {
        _senders = senders;
    }

    public async Task SendAsync(NotificationChannel channel, string to, string message)
    {
        var sender = _senders.FirstOrDefault(s => s.Channel == channel);

        if (sender == null)
            throw new Exception($"No sender found for {channel}");

        await sender.SendAsync(to, message);
    }
}