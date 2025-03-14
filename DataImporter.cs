using System;
using System.Collections.Generic;
using System.IO;

namespace KR_1_MELNIK
{
    public abstract class DataImporter
    {
        public List<T> Import<T>(string filePath)
        {
            string data = File.ReadAllText(filePath);
            return ParseData<T>(data);
        }

        protected abstract List<T> ParseData<T>(string data);
    }
}