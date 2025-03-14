namespace KR_1_MELNIK;

using System;
using System.Diagnostics;

public class TimingDecorator : ICommand
{
    private readonly ICommand _command;

    public TimingDecorator(ICommand command)
    {
        _command = command;
    }

    public void Execute()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        _command.Execute();
        stopwatch.Stop();
        Console.WriteLine($"Команда выполнилась за {stopwatch.ElapsedMilliseconds} мс");
    }
}
