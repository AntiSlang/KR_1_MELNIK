using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace KR_1_MELNIK
{
    public class CsvImporter : DataImporter
    {
        protected override List<T> ParseData<T>(string data)
        {
            var lines = data.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var result = new List<T>();

            foreach (var line in lines.Skip(1))
            {
                var values = line.Split(',');
                var obj = Activator.CreateInstance<T>();

                var properties = typeof(T).GetProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    var property = properties[i];
                    var value = values[i];

                    if (property.PropertyType == typeof(BankAccount))
                    {
                        int accountId = int.Parse(value);
                        var account = new BankAccount();
                        account.id = accountId;
                        property.SetValue(obj, account);
                    }
                    else if (property.PropertyType == typeof(Category))
                    {
                        int categoryId = int.Parse(value);
                        var category = new Category();
                        category.id = categoryId;
                        property.SetValue(obj, category);
                    }
                    else
                    {
                        var convertedValue = Convert.ChangeType(value, property.PropertyType, CultureInfo.InvariantCulture);
                        property.SetValue(obj, convertedValue);
                    }
                }
                result.Add(obj);
            }
            return result;
        }
    }
}