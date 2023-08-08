namespace HW22
{
    internal class Program
    {
        static void Main()
        {
            Tree<Employee> _tree = new Tree<Employee>();
            while (true)
            {
                InputEmployee(_tree);
                Console.WriteLine(Environment.NewLine);
                while (true)
                {
                    Console.Write("Сохранить сотрудников в файл? y/n: ");
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    if (keyInfo.KeyChar == 'y')
                    {
                        //CleanConsole();
                        //SafeToFile(_tree);
                        break;
                    }
                    else if (keyInfo.KeyChar == 'n')
                    {
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Write(new String(' ', 120));
                        Console.SetCursorPosition(0, Console.CursorTop);
                        break;
                    }
                    else 
                    {
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Write(new String(' ',120));
                        Console.SetCursorPosition(0, Console.CursorTop);
                    }
                }
                PrintEmployee(_tree);
                Console.WriteLine(Environment.NewLine);
                FindEmployee(_tree);
                Console.WriteLine(Environment.NewLine);
                while (true)
                {
                    Console.Write("Ввести цифру 0 - переход к началу программы или 1 - снова поиск зарплаты: ");
                    var input = int.TryParse(Console.ReadLine(), out int res);
                    if (input && res == 0)
                    {
                        _tree.Clear();
                        Console.Clear();
                        break;
                    }
                    else if (input && res == 1)
                    {
                        CleanConsole();
                        Console.WriteLine(Environment.NewLine);
                        FindEmployee(_tree);
                    }  
                    else CleanConsole();
                }
            }
        }
        static void InputEmployee(Tree<Employee> tree)
        {
            while (true)
            {
                Console.Write("Введите имя сотрудника или нажмите Enter для выхода: ");
                string? name = Console.ReadLine();
                if (name == "")
                    break;
                bool isValidSalary = false;
                Console.Write("Введите размер зараплаты сотрудника: ");
                do
                {
                    isValidSalary = int.TryParse(Console.ReadLine(), out int salary);
                    if (isValidSalary)
                        tree.Add(new Employee(name, salary));
                    else
                    {
                        CleanConsole();
                        Console.Write("Значеие ЗП должно быть целым положительным числом, " +
                        "введите размер ЗП еще раз: ");
                    }
                }
                while (!isValidSalary);
            }
        }
        static void PrintEmployee(Tree<Employee> tree)
        {
            if (tree.Count == 0)
            {
                Console.WriteLine("Дерево не содержит ни одного элемента");
                return;
            }
            Console.WriteLine("Вывод имен сотрудников и их зарплат в порядке возрастания зарплат");
            foreach (Employee emp in tree)
                Console.WriteLine($"{emp.Name} - {emp.Salary}");
        }
        static void FindEmployee(Tree<Employee> tree)
        {
            if (tree.Count == 0)
            {
                Console.WriteLine("Дерево не содержит ни одного элемента");
                return;
            }
            Console.Write("Введите зарплату сотрудника: ");
            bool isValidSalary = false;
            Employee employee = null;
            do
            {
                isValidSalary = int.TryParse(Console.ReadLine(), out int salary);
                if (isValidSalary)
                {
                    employee = tree.Find(x => x.Salary.CompareTo(salary));
                    string s = employee != null ? $"Такую ЗП имеет сотрудник {employee.Name}" : "сотрудник c такой ЗП не найден";
                    Console.WriteLine(s);
                }
                else
                {
                    CleanConsole();
                    Console.Write("Значеие ЗП должно быть целым положительным числом, " +
                    "введите размер ЗП еще раз: ");
                }
            }
            while (!isValidSalary);
        }
        static void CleanConsole()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine(new String(' ', 120));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
    }
}