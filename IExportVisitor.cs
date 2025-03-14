namespace KR_1_MELNIK;

public interface IExportVisitor
{
    void Visit(BankAccount account);
    void Visit(Category category);
    void Visit(Operation operation);

    void SaveToFiles(string extension);
}
