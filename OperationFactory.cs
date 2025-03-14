namespace KR_1_MELNIK;

public class OperationFactory
{
    private readonly IValidationService _validationService;

    public OperationFactory(IValidationService validationService)
    {
        _validationService = validationService ?? throw new ArgumentNullException(nameof(validationService));
    }
    public Operation CreateOperation(int id, bool type, BankAccount bank_account_id, int amount, DateTime date, string description, Category category_id)
    {
        if (!_validationService.IsAmountValid(amount))
        {
            throw new ArgumentException("Amount must be positive");
        }
        return new Operation(id, type, bank_account_id, amount, date, description, category_id);
    }
}