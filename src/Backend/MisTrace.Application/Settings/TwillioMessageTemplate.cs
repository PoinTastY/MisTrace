using System;

namespace MisTrace.Application.Settings;

public class TwillioMessageTemplate
{
    public string? TemplateName { get; init; }
    public required string TemplateSID { get; set; }
    public int VariablesCount { get; init; }
    public required string Body { get; init; }
    public required ICollection<string> Variables { get; init; }
}
