using CoachingAutomation.Models;

namespace CoachingAutomation.Services;

public class NotificationService
{
    public List<(string Phone, string Message)> GenerateMessages(List<Student> students)
    {
        var messages = new List<(string, string)>();

        foreach (var s in students)
        {
            if (s.Attendance?.ToLower() == "absent")
            {
                messages.Add((s.ParentPhone,
                    $"Your child {s.StudentName} was absent today."));
            }

            if (s.FeesDue > 0)
            {
                messages.Add((s.ParentPhone,
                    $"Reminder: ₹{s.FeesDue} fees pending."));
            }
        }

        return messages;
    }
}