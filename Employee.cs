using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW22
{
    /// <summary>
    /// Класс сотрудников
    /// </summary>
    public class Employee: IComparable<Employee>
    {
        public Employee(string name, int salary)
        {
            Name = name;
            Salary = salary;
        }
        public string Name { get; private set; }
        public int Salary { get; private set; }
        public int CompareTo(Employee? other)
        {
            return Salary.CompareTo(other.Salary);
        }
    }
}
