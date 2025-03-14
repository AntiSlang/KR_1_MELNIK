using Microsoft.Extensions.DependencyInjection;

namespace KR_1_MELNIK;

/// <summary>
/// Реализация паттерна "Фасад"
/// </summary>
public class FinancialFacade
{
    public List<BankAccount> accounts = new();
    public List<Category> categories = new();
    public List<Operation> operations = new();

    public BankAccount CreateAccount(int id, string name, int balance)
    {
        var account = new BankAccount(id, name, balance);
        accounts.Add(account);
        return account;
    }

    public Category CreateCategory(int id, bool type, string name)
    {
        var category = new Category(id, type, name);
        categories.Add(category);
        return category;
    }

    public Operation CreateOperation(int id, bool type, BankAccount bank_account_id, int amount, DateTime date, string description, Category category_id)
    {
        var serviceProvider = new ServiceCollection()
            .AddTransient<IValidationService, ValidationService>()
            .AddTransient<OperationFactory>()
            .BuildServiceProvider();
        var operationFactory = serviceProvider.GetService<OperationFactory>();
        var operation = operationFactory.CreateOperation(id, type, bank_account_id, amount, date, description, category_id);
        operation.Execute();
        operations.Add(operation);
        return operation;
    }

    public List<T> ImportData<T>(string filePath, DataImporter importerExporter)
    {
        return importerExporter.Import<T>(filePath);
    }

    public void AllData(string extension = "json", bool export = false)
    {
        if (export)
        {
            IExportVisitor visitor;
            switch (extension)
            {
                case "json":
                    visitor = new JsonExportVisitor();
                    break;
                case "csv":
                    visitor = new CsvExportVisitor();
                    break;
                default:
                    visitor = new YamlExportVisitor();
                    break;
            }
            foreach (var account in accounts)
                account.Accept(visitor);
            foreach (var category in categories)
                category.Accept(visitor);
            foreach (var operation in operations)
                operation.Accept(visitor);
            visitor.SaveToFiles(extension);
            Console.WriteLine($"Файлы accounts.{extension}, categories.{extension} и operations.{extension} успешно созданы");
            return;
        }
            switch (extension)
            {
                case "json":
                    var jsonImporter = new JsonImporter();
                    accounts = ImportData<BankAccount>($"accounts.{extension}", jsonImporter);
                    categories = ImportData<Category>($"categories.{extension}", jsonImporter);
                    operations = ImportData<Operation>($"operations.{extension}", jsonImporter);
                    break;
                case "csv":
                    var csvImporter = new CsvImporter();
                    accounts = ImportData<BankAccount>($"accounts.{extension}", csvImporter);
                    categories = ImportData<Category>($"categories.{extension}", csvImporter);
                    operations = ImportData<Operation>($"operations.{extension}", csvImporter);
                    break;
                default:
                    var yamlImporter = new YamlImporter();
                    accounts = ImportData<BankAccount>($"accounts.{extension}", yamlImporter);
                    categories = ImportData<Category>($"categories.{extension}", yamlImporter);
                    operations = ImportData<Operation>($"operations.{extension}", yamlImporter);
                    break;
            }
            Console.WriteLine($"Информация из файлов загружена");
        
    }
}
