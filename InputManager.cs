namespace KR_1_MELNIK;

public static class InputManager
{
    public static string InputString(string suggestion)
    {
        string? ret = "";
        while (ret is not null && ret == "")
        {
            Console.WriteLine($"Введите строку - {suggestion}: ");
            ret = Console.ReadLine();
        }
        return ret;
    }
    
    public static int InputInt(string suggestion, Func<int, bool>? validation = null)
    {
        int ret;
        Console.WriteLine($"Введите целое число - {suggestion}: ");
        while (!(int.TryParse(Console.ReadLine(), out ret) && (validation is null || validation(ret))))
        {
            Console.WriteLine($"Введите целое число - {suggestion}: ");
        }
        return ret;
    }
    
    public static DateTime InputDate(string suggestion)
    {
        DateTime ret;
        Console.WriteLine($"Введите дату в формате dd.MM.yyyy - {suggestion}: ");
        while (!DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out ret))
        {
            Console.WriteLine($"Введите дату в формате dd.MM.yyyy - {suggestion}: ");
        }
        return ret;
    }
}