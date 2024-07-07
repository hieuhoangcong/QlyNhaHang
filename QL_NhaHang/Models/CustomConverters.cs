using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public static class CustomConverters
{
    public static ValueConverter<TimeOnly, TimeSpan> TimeOnlyConverter => new ValueConverter<TimeOnly, TimeSpan>(
        v => v.ToTimeSpan(),
        v => TimeOnly.FromTimeSpan(v));
}
