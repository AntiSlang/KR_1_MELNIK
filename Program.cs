namespace KR_1_MELNIK;

class Program
    {
        static void Main()
        {
            var facade = new FinancialFacade();

            while (true)
            {
                Console.WriteLine("\n1) Создать счёт\n2) Создать категорию\n3) Создать операцию\n4) Редактировать счёт\n5) Редактировать категорию\n6) Редактировать операцию\n7) Удалить счёт\n8) Удалить категорию\n9) Удалить операцию\n10) Подсчет разницы доходов и расходов за период\n11) Группировка доходов и расходов по категориям\n12) Загрузка / выгрузка из файлов\n13) Список созданного\n14) Выход");
                int choice = InputManager.InputInt("категория меню", x => x >= 1 && x <= 14);
                if (choice == 14)
                {
                    return;
                }
                ExecuteCommand(new Command(facade, choice));
            }
        }

        public static void ExecuteCommand(ICommand command)
        {
            var timedCommand = new TimingDecorator(command);
            timedCommand.Execute();
        }
    }