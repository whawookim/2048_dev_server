using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;
using System.Collections.Generic;

public static class ValueConverters
{
    public static readonly ValueConverter<List<string>, string> StringListToJson =
        new(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
            v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null) ?? new()
        );
}