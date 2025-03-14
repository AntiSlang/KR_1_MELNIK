namespace KR_1_MELNIK;

public class Command : ICommand
{
    private FinancialFacade facade;
    private int number;

    public Command(FinancialFacade _facade, int _number)
    {
        facade = _facade;
        number = _number;
    }

    public void Execute()
    {
        switch (number)
        {
            case 1:
                menu_create_account();
                break;

            case 2:
                menu_create_category();
                break;

            case 3:
                menu_create_operation();
                break;
                    
            case 4:
                menu_edit_account();
                break;

            case 5:
                menu_edit_category();
                break;

            case 6:
                menu_edit_operation();
                break;
                    
            case 7:
                menu_delete_account();
                break;

            case 8:
                menu_delete_category();
                break;

            case 9:
                menu_delete_operation();
                break;
                    
            case 10:
                menu_stats_operations();
                break;
                    
            case 11:
                menu_stats_groups();
                break;
                    
            case 12:
                menu_files_data();
                break;
                    
            case 13:
                menu_list();
                break;
                    
            default:
                return;
        }
    }

    public void menu_create_account()
    {
        string accountName = InputManager.InputString("название счёта");
        int balance = InputManager.InputInt("начальный баланс", x => x >= 0);
        var account = facade.CreateAccount(facade.accounts.Count, accountName, balance);
        Console.WriteLine($"Счет {account} создан с id = {account.id}");
    }

    public void menu_edit_account()
    {
        if (facade.accounts.Count == 0)
        {
            Console.WriteLine("Для начала создайте счёт");
            return;
        }
        string ids = "";
        foreach (BankAccount i in facade.accounts)
        {
            ids += $"{i.id} ";
        }
        ids = ids.Trim();
        int accountId = -1;
        while (facade.accounts.All(i => i.id != accountId))
        {
            accountId = InputManager.InputInt($"ID счёта для редактирования (доступные ID: {ids})", x => x >= 0);
        }
        string accountName = InputManager.InputString("название счёта");
        int balance = InputManager.InputInt("начальный баланс", x => x >= 0);
        facade.accounts.Find(i => i.id == accountId).balance = balance;
        facade.accounts.Find(i => i.id == accountId).name = accountName;
        Console.WriteLine($"Счет {facade.accounts.Find(i => i.id == accountId)} с id = {accountId} отредактирован");
    }
    public void menu_delete_account()
    {
        if (facade.accounts.Count == 0)
        {
            Console.WriteLine("Для начала создайте счёт");
            return;
        }
        string ids = "";
        foreach (BankAccount i in facade.accounts)
        {
            ids += $"{i.id} ";
        }
        ids = ids.Trim();
        int accountId = -1;
        while (facade.accounts.All(i => i.id != accountId))
        {
            accountId = InputManager.InputInt($"ID счёта для удаления (доступные ID: {ids})", x => x >= 0);
        }
        facade.accounts.RemoveAll(i => i.id == accountId);
        Console.WriteLine($"Счет с id = {accountId} удалён");
    }

    public void menu_create_category()
    {
        string categoryName = InputManager.InputString("название категории");
        bool type = InputManager.InputInt("расходы = 0, доходы = 1", x => x == 0 || x == 1) == 1;
        var category = facade.CreateCategory(facade.categories.Count, type, categoryName);
        Console.WriteLine($"Категория {category} создана с id = {category.id}");
    }
    public void menu_edit_category()
    {
        if (facade.categories.Count == 0)
        {
            Console.WriteLine("Для начала создайте категорию");
            return;
        }
        string ids = "";
        foreach (Category i in facade.categories)
        {
            ids += $"{i.id} ";
        }
        ids = ids.Trim();
        int categoryId = -1;
        while (facade.categories.All(i => i.id != categoryId))
        {
            categoryId = InputManager.InputInt($"ID категории для редактирования (доступные ID: {ids})", x => x >= 0);
        }
        string categoryName = InputManager.InputString("название категории");
        bool type = InputManager.InputInt("расходы = 0, доходы = 1", x => x == 0 || x == 1) == 1;
        facade.categories.Find(i => i.id == categoryId).name = categoryName;
        facade.categories.Find(i => i.id == categoryId).type = type;
        Console.WriteLine($"Категория {facade.categories.Find(i => i.id == categoryId)} с id = {categoryId} отредактирована");
    }
    public void menu_delete_category()
    {
        if (facade.categories.Count == 0)
        {
            Console.WriteLine("Для начала создайте категорию");
            return;
        }
        string ids = "";
        foreach (Category i in facade.categories)
        {
            ids += $"{i.id} ";
        }
        ids = ids.Trim();
        int categoryId = -1;
        while (facade.categories.All(i => i.id != categoryId))
        {
            categoryId = InputManager.InputInt($"ID категории для удаления (доступные ID: {ids})", x => x >= 0);
        }

        facade.categories.RemoveAll(i => i.id == categoryId);
        Console.WriteLine($"Категория с id = {categoryId} удалена");
    }

    public void menu_create_operation()
    {
        if (facade.accounts.Count * facade.categories.Count == 0)
        {
            Console.WriteLine("Для начала создайте счёт и категорию");
            return;
        }
        string ids = "";
        foreach (BankAccount i in facade.accounts)
        {
            ids += $"{i.id} ";
        }
        string ids_categories = "";
        foreach (Category i in facade.categories)
        {
            ids_categories += $"{i.id} ";
        }

        ids = ids.Trim();
        ids_categories = ids_categories.Trim();

        int accountId = -1;
        while (facade.accounts.All(i => i.id != accountId))
        {
            accountId = InputManager.InputInt($"ID счёта (доступные ID: {ids})", x => x >= 0);
        }
        int categoryId = -1;
        while (facade.categories.All(i => i.id != categoryId))
        {
            categoryId = InputManager.InputInt($"ID категории (доступные ID: {ids_categories})", x => x >= 0);
        }
        int amount = InputManager.InputInt("сумма", x => x >= 0);
        string desc = InputManager.InputString("описание");
        DateTime date = InputManager.InputDate("дата операции");
        var operation = facade.CreateOperation(facade.operations.Count, facade.categories.Find(i => i.id == categoryId).type, facade.accounts.Find(i => i.id == accountId), amount, date, desc, facade.categories.Find(i => i.id == categoryId));
        Console.WriteLine($"Операция создана с id = {operation.id}");
    }
    public void menu_edit_operation()
    {
        if (facade.operations.Count == 0)
        {
            Console.WriteLine("Для начала создайте операцию");
            return;
        }
        if (facade.accounts.Count * facade.categories.Count == 0)
        {
            Console.WriteLine("Для начала создайте счёт и категорию");
            return;
        } 
        string ids = "";
        foreach (BankAccount i in facade.accounts)
        {
            ids += $"{i.id} ";
        }
        string ids_categories = "";
        foreach (Category i in facade.categories)
        {
            ids_categories += $"{i.id} ";
        }
        string ids_operations = "";
        foreach (Operation i in facade.operations)
        {
            ids_operations += $"{i.id} ";
        }

        ids = ids.Trim();
        ids_categories = ids_categories.Trim();
        ids_operations = ids_operations.Trim();
        
        int operationId = -1;
        while (facade.operations.All(i => i.id != operationId))
        {
            operationId = InputManager.InputInt($"ID операции для редактирования (доступные ID: {ids_operations})", x => x >= 0);
        }
        int accountId = -1;
        while (facade.accounts.All(i => i.id != accountId))
        {
            accountId = InputManager.InputInt($"ID счёта (доступные ID: {ids})", x => x >= 0);
        }
        int categoryId = -1;
        while (facade.categories.All(i => i.id != categoryId))
        {
            categoryId = InputManager.InputInt($"ID категории (доступные ID: {ids_categories})", x => x >= 0);
        }
        int amount = InputManager.InputInt("сумма", x => x >= 0);
        string desc = InputManager.InputString("описание");
        facade.operations.Find(i => i.id == operationId).type = facade.categories.Find(i => i.id == categoryId).type;
        facade.operations.Find(i => i.id == operationId).bank_account_id = facade.accounts.Find(i => i.id == accountId);
        facade.operations.Find(i => i.id == operationId).amount = amount;
        facade.operations.Find(i => i.id == operationId).date = InputManager.InputDate("дата операции");
        facade.operations.Find(i => i.id == operationId).description = desc;
        facade.operations.Find(i => i.id == operationId).category_id = facade.categories.Find(i => i.id == categoryId);
        Console.WriteLine($"Операция {facade.operations.Find(i => i.id == operationId)} с id = {operationId} отредактирована");
    }
    public void menu_delete_operation()
    {
        if (facade.operations.Count == 0)
        {
            Console.WriteLine("Для начала создайте операцию");
            return;
        }
        if (facade.accounts.Count * facade.categories.Count == 0)
        {
            Console.WriteLine("Для начала создайте счёт и категорию");
            return;
        }
        string ids_operations = "";
        foreach (Operation i in facade.operations)
        {
            ids_operations += $"{i.id} ";
        }

        ids_operations = ids_operations.Trim();
        
        int operationId = -1;
        while (facade.operations.All(i => i.id != operationId))
        {
            operationId = InputManager.InputInt($"ID операции для удаления (доступные ID: {ids_operations})", x => x >= 0);
        }
        facade.operations.RemoveAll(i => i.id == operationId);
        Console.WriteLine($"Операция с id = {operationId} удалена");
    }

    public void menu_stats_operations()
    {
        if (facade.operations.Count == 0)
        {
            Console.WriteLine("Для начала создайте операцию");
            return;
        }
        DateTime startDate = InputManager.InputDate("начальная дата периода");
        DateTime endDate = InputManager.InputDate("конечная дата периода");
        var operationsInPeriod = facade.operations.FindAll(x => x.date >= startDate && x.date <= endDate);
        if (operationsInPeriod.Count == 0)
        {
            Console.WriteLine("В указанный период операций не найдено");
            return;
        }
        int plus = operationsInPeriod.FindAll(x => x.type).Sum(x => x.amount);
        int minus = operationsInPeriod.FindAll(x => !x.type).Sum(x => -x.amount);
        Console.WriteLine($"Доходы: {plus}, расходы: {minus}, итого: {plus + minus}");
    }

    public void menu_stats_groups()
    {
        if (facade.categories.Count == 0)
        {
            Console.WriteLine("Для начала создайте категорию");
        }
        string answer = "";
        foreach (var category in facade.categories)
        {
            int plus = facade.operations.FindAll(x => x.category_id.id == category.id && x.type).Sum(x => x.amount);
            int minus = facade.operations.FindAll(x => x.category_id.id == category.id && !x.type).Sum(x => -x.amount);
            answer += $"Категория {category.name} - доходы: {plus}, расходы: {minus}, итого: {plus + minus}; ";
        }
        Console.WriteLine(answer.Trim());
    }

    public void menu_files_data()
    {
        int extension_int = InputManager.InputInt("json = 0, csv = 1, yaml = 2", x => x >= 0 && x <= 2);
        string extension = extension_int == 0 ? "json" : extension_int == 1 ? "csv" : "yaml";
        bool export = InputManager.InputInt("import = 0, export = 1", x => x == 0 || x == 1) == 1;
        facade.AllData(extension, export);
    }

    public void menu_list()
    {
        if (facade.accounts.Count + facade.categories.Count + facade.operations.Count == 0)
        {
            Console.WriteLine("Для начала создайте что-либо");
            return;
        } 
        string result = "Счета:";
        foreach (var account in facade.accounts)
        {
            result += $" [имя:{account.name} id:{account.id} баланс:{account.balance}]";
        }
        result += "\nКатегории:";
        foreach (var category in facade.categories)
        {
            result += $" [id:{category.id} тип:{category.type} имя:{category.name}]";
        }
        result += "\nОперации:";
        foreach (var operation in facade.operations)
        {
            result += $" [id:{operation.id} тип:{operation.type} описание:{operation.description} amount:{operation.amount} дата:{operation.DateString()}]";
        }
        Console.WriteLine(result);
    }
}
