using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Authentication.ExtendedProtection;

namespace DelegateTest
{
    /// <summary>
    /// Lambda表达式
    /// </summary>
    public class LookLambda
    {

        Action<int, int> Add = delegate (int i, int j) { Console.WriteLine($"{i}+{j}={i + j}"); };

        //简化
        Action<int, int> AddI = (int i, int j) => Console.WriteLine($"{i}+{j}={i + j}");

        //简化参数类型
        Action<int, int> AddII = (i, j) => Console.WriteLine($"{i}+{j}={i + j}");

        //返回值的代码块只有一行的话可直接省略大括号
        Func<int, int, string> AddIII = (i, j) => $"{i}+{j}={i + j}";
    }

    public class ListExtensionMethod
    {
        List<Employee> employeeCH = new List<Employee>();

        List<Employee> employeeUK = new List<Employee>();

        List<Employee> employeeHK = new List<Employee>();
        public ListExtensionMethod()
        {
            employeeCH.Add(new Employee { Name = "Li", BirthDate = DateTime.Parse("1997.02.15") });

            employeeUK.Add(new Employee { Name = "Victoria", BirthDate = DateTime.Parse("1819.05.24") });

            employeeHK.Add(new Employee { Name = "Bruce Lee", BirthDate = DateTime.Parse("1940.11.27") });
        }



        public bool BithdateAtOneNineNineSeven()
        {
            //高级集合扩展方法一，是否包含
            return employeeCH.Any(e => e.BirthDate.Year == 1997);
        }

        public IEnumerable<Employee> SkipTake()
        {
            //高级集合扩展方法二，跳过+获取
            return employeeCH.Skip(0).Take(1);
        }

        public Employee YoungestEmp()
        {
            var employees = employeeCH.Intersect(employeeUK).Intersect(employeeHK);
            //集合常用扩展方法之一
            //FirstOrDefault (获取第一个，如果—个都没有则返回默认值）
            return employees.FirstOrDefault(e => e.BirthDate == employees.Min(r => r.BirthDate));
        }

        public Employee YoungestEmpI()
        {
            var employees = employeeCH.Intersect(employeeUK).Intersect(employeeHK);
            //SingleOrDefoult(获取唯一一个， 如果没有则返回默认值，如果有多个则异常）
            //集合常用扩展方法之二
            return employees.SingleOrDefault(e => e.BirthDate == employees.Min(r => r.BirthDate));
        }
    }

    public class Employee
    {
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
