namespace HW22
{
    internal class Program
    {
        static void Main()
        {
            Tree<Employee> tree = new Tree<Employee>();
            //tree.Add(new Employee("Petr", 1000));
            //tree.Add(new Employee("Vasiliy", 1200));
            //tree.Add(new Employee("Andrey", 1100));
            //tree.Add(new Employee("Vladimir", 900));
            //tree.Add(new Employee("Alex", 800));
            //tree.Add(new Employee("Elena", 1300));
            //tree.Add(new Employee("Nikolay", 700));
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
            Console.ReadLine(); 
        }
    }
}