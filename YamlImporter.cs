using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace KR_1_MELNIK
{
    public class YamlImporter : DataImporter
    {
        private readonly IDeserializer _deserializer;

        public YamlImporter()
        {

            _deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
        }

        protected override List<T> ParseData<T>(string data)
        {
            return _deserializer.Deserialize<List<T>>(data);
        }
    }
}