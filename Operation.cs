namespace KR_1_MELNIK;

public class Operation
{
    public int id { get; set; }
    public bool type { get; set; }
    public BankAccount bank_account_id { get; set; }
    public int amount { get; set; }
    public DateTime date { get; set; }
    public string description { get; set; } = "";
    public Category category_id { get; set; }

    public Operation(int _id, bool _type, BankAccount _bank_account_id, int _amount, DateTime _date,
        string _description, Category _category_id) =>
        (id, type, bank_account_id, amount, date, description, category_id) = (_id, _type, _bank_account_id, _amount,
            _date, _description, _category_id);

    public Operation() => (id, type, bank_account_id, amount, date, description, category_id) = (new Random().Next(100000, 1000000), true,
        new BankAccount(), 0,
        DateTime.Now, "Operation", new Category());

    public string DateString()
    {
        return date.ToString("dd.MM.yyyy");
    }

    public void Execute()
    {
        if (type)
        {
            bank_account_id.Deposit(amount);
        }
        else
        {
            bank_account_id.Withdraw(amount);
        }
    }
    public override string ToString()
    {
        return $"{bank_account_id} - {category_id} - {amount}";
    }
    public void Accept(IExportVisitor visitor)
    {
        visitor.Visit(this);
    }
}