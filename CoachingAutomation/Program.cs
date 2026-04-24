using CoachingAutomation.Services;
using CoachingAutomation.Jobs;
using Hangfire;
using Hangfire.MemoryStorage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.Configure<TwilioSettings>(
    builder.Configuration.GetSection("Twilio"));
    
builder.Services.Configure<GoogleSettings>(
    builder.Configuration.GetSection("Google"));

// Services
builder.Services.AddSingleton<GoogleSheetService>();
builder.Services.AddSingleton<NotificationService>();
builder.Services.AddSingleton<WhatsAppService>();
builder.Services.AddSingleton<NotificationJob>();

// Hangfire
builder.Services.AddHangfire(config =>
    config.UseMemoryStorage());

builder.Services.AddHangfireServer();

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

// Hangfire Dashboard
app.UseHangfireDashboard();

// Schedule job (daily 7 PM)
RecurringJob.AddOrUpdate<NotificationJob>(
    "daily-job",
    job => job.Run(),
    "0 19 * * *"
);

app.Run();