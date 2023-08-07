namespace HW22
{
    internal class Program
    {
        static void Main()
        {
            Tree<Employee> tree = new Tree<Employee>();

            while (true)
            {
                Console.Write("Введите имя сотрудника или нажмите Enter для выхода: ");
                string? name = Console.ReadLine();
                if(name == "")
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
                        Console.SetCursorPosition(0, Console.CursorTop-1);
                        Console.WriteLine(new String(' ',120));
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.Write("Значеие ЗП должно быть целым положительным числом, " +
                        "введите размер ЗП еще раз: ");
                    }
                }
                while (!isValidSalary);
                Console.WriteLine(Environment.NewLine);
            }
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Вывод имен сотрудников и их зарплат в порядке возрастания зарплат");
            foreach (Employee emp in tree)
                Console.WriteLine($"{emp.Name} - {emp.Salary}");
            Console.ReadLine(); 
        }
    }
}