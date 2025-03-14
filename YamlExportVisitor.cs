using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace KR_1_MELNIK;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class YamlExportVisitor : IExportVisitor
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
        ISerializer _serializer = new SerializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        return _serializer.Serialize(data);
    }
}

