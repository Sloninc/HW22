using System.Text.Json;

namespace HW22
{
    internal class Program
    {
        static async Task Main()
        {
            Tree<Employee> _tree = new Tree<Employee>();
            while (true)
            {
                while (true)
                {
                    Console.Write("Загрузить сотрудников из файла? y/n: ");
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    if (keyInfo.KeyChar == 'y')
                    {
                        CleanConsole(Console.CursorTop);
                        ReadFromFile(_tree);
                        break;
                    }
                    else if (keyInfo.KeyChar == 'n')
                    {
                        CleanConsole(Console.CursorTop);
                        InputEmployee(_tree);
                        break;
                    }
                    else
                    {
                        CleanConsole(Console.CursorTop);
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
                        CleanConsole(Console.CursorTop - 1);
                        Console.WriteLine(Environment.NewLine);
                        FindEmployee(_tree);
                    }  
                    else CleanConsole(Console.CursorTop - 1);
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
                    SafeToFile(tree);
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
                    CleanConsole(Console.CursorTop - 1);
                    Console.Write("Значеие ЗП должно быть целым положительным числом, " +
                    "введите размер ЗП еще раз: ");
                }
            }
            while (!isValidSalary);
        }
        static async Task SafeToFile(Tree<Employee> tree) 
        {
            if (tree.Count == 0)
            {
                Console.WriteLine("Дерево не содержит ни одного элемента");
                return;
            }
            using (FileStream fs = new FileStream("Employees.json", FileMode.OpenOrCreate))
            {
                foreach(Employee emp in tree)
                {
                    await JsonSerializer.SerializeAsync<Employee>(fs, emp);
                }
                Console.WriteLine("Данные сотрудников были сохранены в файл Employees.json");
            }
        }
        static async Task ReadFromFile(Tree<Employee> tree)
        {
            using (FileStream fs = new FileStream("Employees.json", FileMode.OpenOrCreate))
            {
                while (fs.Position < fs.Length)
                {
                    var emp = await JsonSerializer.DeserializeAsync<Employee>(fs);
                    tree.Add(emp);
                }
            }
        }
        static void CleanConsole(int row)
        {
            Console.SetCursorPosition(0, row);
            Console.WriteLine(new String(' ', 120));
            Console.SetCursorPosition(0, row);
        }
    }
}