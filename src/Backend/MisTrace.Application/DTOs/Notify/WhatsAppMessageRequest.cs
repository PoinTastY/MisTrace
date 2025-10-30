using System;

namespace MisTrace.Application.DTOs.Notify;

public record WhatsAppMessageRequest
{
    public required string To { get; init; }
    public string? TemplateName { get; init; }
    public string? TemplateSID { get; init; }
    public required Dictionary<string, string> Variables { get; init; }
}
