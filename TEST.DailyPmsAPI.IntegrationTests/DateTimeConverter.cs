using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TEST.DailyPmsAPI.IntegrationTests
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.SpecifyKind(DateTime.Parse(reader.GetString()!), DateTimeKind.Utc);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(DateTime.SpecifyKind(value, DateTimeKind.Utc));
        }

        //USE LIKE THIS :
        //JsonSerializerOptions options = new JsonSerializerOptions();

        //options.Converters.Add(new DateTimeConverter());

        //var response = JsonSerializer.Deserialize<Testclass>(json, options);

        //var responseJson = JsonSerializer.Serialize(response, options);
    }
}

