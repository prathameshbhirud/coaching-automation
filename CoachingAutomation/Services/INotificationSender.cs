using CoachingAutomation.Models;

public interface INotificationSender
{
    NotificationChannel Channel { get; }
    Task SendAsync(string to, string message);
}