namespace KR_1_MELNIK;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class CsvExportVisitor : IExportVisitor
{
    private List<BankAccount> _accounts = new();
    private List<Category> _categories = new();
    private List<Operation> _operations = new();

    public void Visit(BankAccount account)
    {
        _accounts.Add(account);
    }

    public void Visit(Category category)
    {
        _categories.Add(category);
    }

    public void Visit(Operation operation)
    {
        _operations.Add(operation);
    }

    public void SaveToFiles(string extension)
    {
        File.WriteAllText($"accounts.{extension}", SerializeData(_accounts));
        File.WriteAllText($"categories.{extension}", SerializeData(_categories));
        File.WriteAllText($"operations.{extension}", SerializeData(_operations));
    }
    
    private string SerializeData<T>(List<T> data)
    {
        var headers = string.Join(",", typeof(T).GetProperties().Select(p => p.Name));
        var rows = data.Select(obj =>
        {
            var values = typeof(T).GetProperties().Select(p =>
            {
                var value = p.GetValue(obj);
                if (value is BankAccount account)
                {
                    return account.id.ToString();
                }
                if (value is Category category)
                {
                    return category.id.ToString();
                }
                return value?.ToString() ?? string.Empty;
            });
            return string.Join(",", values);
        });
        return headers + "\n" + string.Join("\n", rows);
    }
}

