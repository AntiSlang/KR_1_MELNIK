namespace KR_1_MELNIK;

public class BankAccount
{
    public int id { get; set; }
    public string name { get; set; }
    public int balance { get; set; }

    public BankAccount(int _id, string _name, int _balance) => (id, name, balance) = (_id, _name, _balance);
    public BankAccount() => (id, name, balance) = (new Random().Next(100000, 1000000), "BankAccount", 0);

    public void Deposit(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be positive");
        }
        balance += amount;
    }

    public void Withdraw(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be positive");
        }
        if (amount > balance)
        {
            throw new ArgumentException("Not enough balance");
        }
        balance -= amount;
    }
    
    public override string ToString()
    {
        return name;
    }
    
    public void Accept(IExportVisitor visitor)
    {
        visitor.Visit(this);
    }
}