namespace KR_1_MELNIK.Tests;

using Xunit;
using KR_1_MELNIK;

public class Tests
{
    [Fact]
    public void CreateAccount_IdFind()
    {
        var facade = new FinancialFacade();
        facade.CreateAccount(id: 123, name: "test", balance: 1000);
        Assert.Contains(facade.accounts, a => a.id == 123);
    }
    
    [Fact]
    public void CreateAccount_NameFind()
    {
        var facade = new FinancialFacade();
        facade.CreateAccount(id: 123, name: "test", balance: 1000);
        Assert.Contains(facade.accounts, a => a.name == "test");
    }
    
    [Fact]
    public void CreateAccount_BalanceFind()
    {
        var facade = new FinancialFacade();
        facade.CreateAccount(id: 123, name: "test", balance: 1000);
        Assert.Contains(facade.accounts, a => a.balance == 1000);
    }
    
    [Fact]
    public void CreateAccount_IdEditFind()
    {
        var facade = new FinancialFacade();
        facade.CreateAccount(id: 123, name: "test", balance: 1000);
        facade.accounts.Find(i => i.id == 123).id = 567;
        Assert.Contains(facade.accounts, a => a.id == 567);
    }
    
    [Fact]
    public void CreateAccount_NameEditFind()
    {
        var facade = new FinancialFacade();
        facade.CreateAccount(id: 123, name: "test", balance: 1000);
        facade.accounts.Find(i => i.id == 123).name = "qwerty";
        Assert.Contains(facade.accounts, a => a.name == "qwerty");
    }
    
    [Fact]
    public void CreateOperation_IdDelete()
    {
        var facade = new FinancialFacade();
        facade.CreateOperation(123, true, new BankAccount(), 1, DateTime.Now, "", new Category());
        facade.operations.RemoveAll(i => i.amount == 1);
        Assert.DoesNotContain(facade.operations, a => a.id == 123);
    }
    
    [Fact]
    public void CreateCategory_IdDelete()
    {
        var facade = new FinancialFacade();
        facade.CreateCategory(123, true, "test");
        facade.categories.RemoveAll(i => i.name == "test");
        Assert.DoesNotContain(facade.categories, a => a.id == 123);
    }
    
    [Fact]
    public void CreateOperation_IdFind()
    {
        var facade = new FinancialFacade();
        facade.CreateOperation(123, true, new BankAccount(), 1, DateTime.Now, "", new Category());
        Assert.Contains(facade.operations, a => a.id == 123);
    }
    
    [Fact]
    public void CreateOperation_IdEditFind()
    {
        var facade = new FinancialFacade();
        facade.CreateOperation(123, true, new BankAccount(), 1, DateTime.Now, "", new Category());
        facade.operations.Find(i => i.id == 123).id = 567;
        Assert.Contains(facade.operations, a => a.id == 567);
    }
    
    [Fact]
    public void CreateAccount_IdDelete()
    {
        var facade = new FinancialFacade();
        facade.CreateAccount(id: 123, name: "test", balance: 1000);
        facade.accounts.RemoveAll(i => i.name == "test");
        Assert.DoesNotContain(facade.accounts, a => a.id == 123);
    }
    
    [Fact]
    public void CreateCategory_IdFind()
    {
        var facade = new FinancialFacade();
        facade.CreateCategory(123, true, "test");
        Assert.Contains(facade.categories, a => a.id == 123);
    }
    
    [Fact]
    public void CreateCategory_IdEditFind()
    {
        var facade = new FinancialFacade();
        facade.CreateCategory(123, true, "test");
        facade.categories.Find(i => i.id == 123).id = 567;
        Assert.Contains(facade.categories, a => a.id == 567);
    } 
    [Fact]
    public void CreateAccount_BalanceEditFind()
    {
        var facade = new FinancialFacade();
        facade.CreateAccount(id: 123, name: "test", balance: 1000);
        facade.accounts.Find(i => i.id == 123).balance = 2000;
        Assert.Contains(facade.accounts, a => a.balance == 2000);
    }
    
    [Fact]
    public void CreateOperation_NegativeAmount_ThrowsException()
    {
        var facade = new FinancialFacade();
        var account = facade.CreateAccount(1, "test", 1000);
        var category = facade.CreateCategory(1, true, "testCategory");

        Assert.Throws<ArgumentException>(() => 
            facade.CreateOperation(1, true, account, -500, DateTime.Now, "Invalid operation", category)
        );
    }
    
    [Fact]
    public void AccountBalance_CalculatesCorrectly()
    {
        var facade = new FinancialFacade();
        var account = facade.CreateAccount(1, "test", 1000);
        var categoryIncome = facade.CreateCategory(1, true, "Salary");
        var categoryExpense = facade.CreateCategory(2, false, "Shopping");

        facade.CreateOperation(1, true, account, 500, DateTime.Now, "Salary", categoryIncome);
        facade.CreateOperation(2, false, account, 200, DateTime.Now, "Shopping", categoryExpense);

        Assert.Equal(1300, account.balance);
    }
    
    [Fact]
    public void Operations_GroupedByCategory_CorrectSum()
    {
        var facade = new FinancialFacade();
        var account = facade.CreateAccount(1, "test", 1000);
        var salaryCategory = facade.CreateCategory(1, true, "Salary");
        var foodCategory = facade.CreateCategory(2, false, "Food");

        facade.CreateOperation(1, true, account, 1000, DateTime.Now, "Salary", salaryCategory);
        facade.CreateOperation(2, false, account, 300, DateTime.Now, "Lunch", foodCategory);
        facade.CreateOperation(3, false, account, 200, DateTime.Now, "Dinner", foodCategory);

        int totalIncome = facade.operations.Where(o => o.category_id == salaryCategory && o.type).Sum(o => o.amount);
        int totalExpense = facade.operations.Where(o => o.category_id == foodCategory && !o.type).Sum(o => o.amount);

        Assert.Equal(1000, totalIncome);
        Assert.Equal(500, totalExpense);
    }
    
    [Fact]
    public void ExportData_Json_FileExists()
    {
        var facade = new FinancialFacade();
        var account = facade.CreateAccount(1, "test", 1000);
        var category = facade.CreateCategory(1, true, "testCategory");
        facade.CreateOperation(1, true, account, 500, DateTime.Now, "Salary", category);

        facade.AllData("json", export: true);

        Assert.True(File.Exists("accounts.json"));
        Assert.True(File.Exists("categories.json"));
        Assert.True(File.Exists("operations.json"));
    }
    
    [Fact]
    public void ImportData_Json_LoadsCorrectly()
    {
        var facade = new FinancialFacade();
        facade.CreateAccount(123, "test", 1000);
        facade.AllData("json", export: true);

        var jsonImporter = new JsonImporter();
        var accounts = jsonImporter.Import<BankAccount>("accounts.json");

        Assert.NotEmpty(accounts);
    }
    
    [Fact]
    public void ImportData_Csv_LoadsCorrectly()
    {
        var facade = new FinancialFacade();
        facade.CreateAccount(123, "test", 1000);
        facade.AllData("csv", export: true);

        var csvImporter = new CsvImporter();
        var accounts = csvImporter.Import<BankAccount>("accounts.csv");

        Assert.NotEmpty(accounts);
    }
    
    [Fact]
    public void ImportData_Yaml_LoadsCorrectly()
    {
        var facade = new FinancialFacade();
        facade.CreateAccount(123, "test", 1000);
        facade.AllData("yaml", export: true);

        var yamlImporter = new YamlImporter();
        var accounts = yamlImporter.Import<BankAccount>("accounts.yaml");

        Assert.NotEmpty(accounts);
    }
}