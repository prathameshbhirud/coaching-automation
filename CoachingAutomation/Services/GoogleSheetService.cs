using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using CoachingAutomation.Models;
using Microsoft.Extensions.Options;


namespace CoachingAutomation.Services;

public class GoogleSheetService
{
    private readonly GoogleSettings _settings;

    public GoogleSheetService(IOptions<GoogleSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task<List<Student>> GetStudentsAsync()
    {
        var credential = GoogleCredential
            .FromFile(_settings.CredentialsPath)
            .CreateScoped(SheetsService.Scope.SpreadsheetsReadonly);

        var service = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = "CoachingAutomation"
        });

        var request = service.Spreadsheets.Values.Get(_settings.SpreadsheetId, _settings.Range);
        var response = await request.ExecuteAsync();

        var students = new List<Student>();

        foreach (var row in response.Values)
        {
            students.Add(new Student
            {
                StudentName = row[0]?.ToString(),
                ParentPhone = row[1]?.ToString(),
                Attendance = row[2]?.ToString(),
                FeesDue = decimal.TryParse(row[3]?.ToString(), out var fee) ? fee : 0,
                PreferredChannel = Enum.TryParse(row[4]?.ToString(), true, out NotificationChannel ch) ? ch: NotificationChannel.WhatsApp
            });
        }

        return students;
    }
}