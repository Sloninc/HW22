using System.Text.Json;

namespace HW22
{
    internal class Program
    {
        static Tree<Employee>? _tree = new Tree<Employee>();
        static async Task Main()
        {
            while (true)
            {
                while (true)
                {
                    Console.Write("Загрузить сотрудников из файла? y/n: ");
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    if (keyInfo.KeyChar == 'y')
                    {
                        CleanConsole(Console.CursorTop);
                        Tree<Employee> _tree = new Tree<Employee>(); 
                        await ReadFromFile();
                        break;
                    }
                    else if (keyInfo.KeyChar == 'n')
                    {
                        CleanConsole(Console.CursorTop);
                        await InputEmployee();
                        break;
                    }
                    else
                    {
                        CleanConsole(Console.CursorTop);
                    }
                }
                if(_tree.Count != 0)
                {
                    PrintEmployee();
                    Console.WriteLine(Environment.NewLine);
                    FindEmployee();
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
                            CleanConsole(Console.CursorTop - 1);
                            Console.WriteLine(Environment.NewLine);
                            FindEmployee();
                        }
                        else CleanConsole(Console.CursorTop - 1);
                    }
                }
            }
        }
        static async Task InputEmployee()
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
                        _tree.Add(new Employee(name, salary));
                    else
                    {
                        CleanConsole(Console.CursorTop - 1);
                        Console.Write("Значеие ЗП должно быть целым положительным числом, " +
                        "введите размер ЗП еще раз: ");
                    }
                }
                while (!isValidSalary);
            }
            Console.WriteLine(Environment.NewLine);
            while (true)
            {
                Console.Write("Сохранить сотрудников в файл? y/n: ");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.KeyChar == 'y')
                {
                    CleanConsole(Console.CursorTop);
                    await SafeToFile();
                    break;
                }
                else if (keyInfo.KeyChar == 'n')
                {
                    CleanConsole(Console.CursorTop);
                    break;
                }
                else
                {
                    CleanConsole(Console.CursorTop);
                }
            }
        }
        static void PrintEmployee()
        {
            if (_tree.Count == 0)
            {
                Console.WriteLine("Дерево не содержит ни одного элемента");
                return;
            }
            Console.WriteLine("Вывод имен сотрудников и их зарплат в порядке возрастания зарплат");
            foreach (Employee emp in _tree)
                Console.WriteLine($"{emp.Name} - {emp.Salary}");
        }
        static void FindEmployee()
        {
            if (_tree.Count == 0)
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
                    employee = _tree.Find(x => x.Salary.CompareTo(salary));
                    string s = employee != null ? $"Такую ЗП имеет сотрудник {employee.Name}" : "сотрудник c такой ЗП не найден";
                    Console.WriteLine(s);
                }
                else
                {
                    CleanConsole(Console.CursorTop - 1);
                    Console.Write("Значеие ЗП должно быть целым положительным числом, " +
                    "введите размер ЗП еще раз: ");
                }
            }
            while (!isValidSalary);
        }
        static async Task SafeToFile() 
        {
            if (_tree.Count == 0)
            {
                Console.WriteLine("Дерево не содержит ни одного элемента");
                return;
            }
            using (FileStream fs = new FileStream("Employees.json", FileMode.Create))
            {
                var employees = _tree.ToList<Employee>();
                await JsonSerializer.SerializeAsync<List<Employee>>(fs, employees);
                Console.WriteLine("Данные сотрудников были сохранены в файл Employees.json");
                Console.WriteLine(Environment.NewLine);
            }
        }
        static async Task ReadFromFile()
        {
            if (File.Exists("Employees.json"))
            {
                _tree.Clear();
                using (FileStream fs = new FileStream("Employees.json", FileMode.Open))
                {
                    var employees = await JsonSerializer.DeserializeAsync<List<Employee>>(fs);
                    for (int i = 0; i < employees.Count; i++)
                        _tree.Add(employees[i]);
                }
            }
            else Console.WriteLine("Файл Employees.json не найден");
        }
        static void CleanConsole(int row)
        {
            Console.SetCursorPosition(0, row);
            Console.WriteLine(new String(' ', 120));
            Console.SetCursorPosition(0, row);
        }
    }
}