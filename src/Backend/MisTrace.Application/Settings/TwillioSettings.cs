using System;

namespace MisTrace.Application.Settings;

public class TwillioSettings
{
    public required TwillioMessageTemplate[] Templates { get; init; }
}
