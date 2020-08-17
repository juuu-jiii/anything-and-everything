using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinBlank
{
    // This class must be explicitly set to public to avoid inconsistent accessibility errors.
    // Remove the access modifier and keep an eye on the error list.
    public class Employee
    {
        public string Email { get; set; }
        public string FullName { get; set; }

        // Constructor not required - class fields will be altered automatically via their 
        //      properties when JsonConvert.DeserializeObject runs.
        //public Employee(string email, string fullName)
        //{
        //    this.Email = email;
        //    this.FullName = fullName;
        //}
    }
}
