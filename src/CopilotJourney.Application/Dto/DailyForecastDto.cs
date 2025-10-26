using System.Collections.Generic;

namespace CopilotJourney.Application.Dto;

public sealed record DailyForecastDayDto(string Date, double? TempMinC, double? TempMaxC, double? PrecipitationMm);

public sealed record DailyForecastDto(IReadOnlyList<DailyForecastDayDto> Days);
