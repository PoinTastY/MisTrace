using Microsoft.Extensions.Options;
using MisTrace.Application.DTOs.Notify;
using MisTrace.Application.Interfaces;
using MisTrace.Application.Settings;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace MisTrace.Infrastructure.Services.Notification;

public class TwilioService : ITwillioService
{
    private readonly TwillioSettings _twillioSettings;
    private readonly string _accountSid;
    private readonly string _authToken;
    private readonly string _fromWhatsApp;

    public TwilioService(IOptions<TwillioSettings> options)
    {
        _twillioSettings = options.Value;

        _accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNTSID")
            ?? throw new NullReferenceException("Missing environment variable");
        _authToken = Environment.GetEnvironmentVariable("TWILIO_AUTHTOKEN")
            ?? throw new NullReferenceException("Missing environment variable");
        _fromWhatsApp = Environment.GetEnvironmentVariable("TWILIO_FROMWHATSAPP")
            ?? throw new NullReferenceException("Missing environment variable");
    }
    public void SendWhatsAppMessage(WhatsAppMessageRequest request)
    {
        if (request.TemplateName is null && request.TemplateSID is null)
            throw new InvalidOperationException("TemplateName or TemplateSID is required");

        TwilioClient.Init(_accountSid, _authToken);

        TwillioMessageTemplate template = _twillioSettings.Templates
            .FirstOrDefault(t => t.TemplateName == request.TemplateName ||
                t.TemplateSID == request.TemplateSID)
                    ?? throw new KeyNotFoundException($"Provided template not found :c");

        if (request.Variables.Count != template.VariablesCount)
            throw new InvalidOperationException("Provided variables count doesnt match template");

        string body = template.Body;

        foreach (string variableKey in request.Variables.Keys)
        {
            string value = request.Variables[variableKey];

            if (string.IsNullOrEmpty(value))
                throw new InvalidOperationException("One of the provided keys resulted null or empty");

            body = body.Replace($"{{{variableKey}}}", value);
        }

        CreateMessageOptions messageOptions = new(new PhoneNumber(request.To))
        {
            From = new PhoneNumber(_fromWhatsApp),
            ContentSid = template.TemplateSID
        };

        messageOptions.ContentVariables = body;

        MessageResource message = MessageResource.Create(messageOptions);

        Console.WriteLine($"Sent template: {template.TemplateName}.");
    }
}
