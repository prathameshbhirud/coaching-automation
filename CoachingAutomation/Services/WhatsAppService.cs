using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace CoachingAutomation.Services;

public class WhatsAppService
{
    private readonly TwilioSettings _settings;

    public WhatsAppService(IOptions<TwilioSettings> settings)
    {
        _settings = settings.Value;

        TwilioClient.Init(_settings.AccountSid, _settings.AuthToken);
    }

    public void SendMessage(string to, string message)
    {
        MessageResource.Create(
            from: new PhoneNumber(_settings.FromNumber),
            to: new PhoneNumber($"whatsapp:{to}"),
            body: message
        );
    }
}