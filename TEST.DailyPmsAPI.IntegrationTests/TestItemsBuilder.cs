using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using DailyPmsShared;

namespace TEST.DailyPmsAPI.IntegrationTests
{
    public static class TestItemsBuilder<T> where T : class, IEntity
    {
        public static IList<T> BuildTestItems()
        {
            var typeName = typeof(T).Name;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), typeName + "s/Test_" + typeName + "s.json");

            var jsonFile = File.ReadAllText(filePath);
            JsonSerializerOptions options = new();
            options.Converters.Add(new DateTimeConverter());
            var itemsFromJson = JsonSerializer.Deserialize<IList<T>>(jsonFile, options);

            return itemsFromJson!;
        }
    }
}

