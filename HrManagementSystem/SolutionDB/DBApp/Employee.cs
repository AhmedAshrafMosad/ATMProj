using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBApp
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Salary { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; } // Changed to string for gender
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; } // Navigation property
    }

}
