using System;
using System.Collections.Generic;
using System.Text.Json;

namespace KR_1_MELNIK
{
    public class JsonImporter : DataImporter
    {
        protected override List<T> ParseData<T>(string data)
        {
            return JsonSerializer.Deserialize<List<T>>(data);
        }
    }
}