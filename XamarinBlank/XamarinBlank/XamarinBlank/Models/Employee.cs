using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinBlank
{
    class Employee
    {
        public string Email { get; set; }
        public string FullName { get; set; }

        public Employee(string email, string fullName)
        {
            this.Email = email;
            this.FullName = fullName;
        }
    }
}
