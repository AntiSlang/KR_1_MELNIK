namespace KR_1_MELNIK;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class JsonExportVisitor : IExportVisitor
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
        File.WriteAllText($"accounts.{extension}", JsonSerializer.Serialize(_accounts, new JsonSerializerOptions { WriteIndented = true }));
        File.WriteAllText($"categories.{extension}", JsonSerializer.Serialize(_categories, new JsonSerializerOptions { WriteIndented = true }));
        File.WriteAllText($"operations.{extension}", JsonSerializer.Serialize(_operations, new JsonSerializerOptions { WriteIndented = true }));
    }
}

