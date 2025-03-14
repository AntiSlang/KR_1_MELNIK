namespace KR_1_MELNIK;

public class ValidationService : IValidationService
{
    public bool IsAmountValid(int amount)
    {
        return amount > 0;
    }
}