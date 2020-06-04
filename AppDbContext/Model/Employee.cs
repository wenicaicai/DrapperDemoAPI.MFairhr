using AppDbContext.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppDbContext.Model
{
    public class Employee
    {
        public int Id { get; set; }

        public DateTime BirthDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public DateTime HireDate { get; set; }

    }
}
