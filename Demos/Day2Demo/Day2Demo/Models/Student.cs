using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // This is for the line telling the database how to generate values for new entries 
using System.Linq;
using System.Web;

namespace Day2Demo.Models
{
    public class Student
    {
        // Fields
        public string Name { get; private set; }
        public int Year { get; private set; }
        public string College { get; private set; }

        // This line tells the database how to generate values for a new entry - in this case, it generates 
        //      a value each time a row is inserted.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // Guid = globally unique identifier: like the keys in a hashtable - all unique
        // This is an ideal type for a primary key property, as entries in a database must be unique.
        // In this case, Guid StudentIdentifier will serve as the primary key property for this model class.
        public Guid Id { get; private set; }
        // A note on the above field + command pair: VS automatically looks for a field named Id - if the primary key
        //      field does not follow the naming convention, an additional command [Key] must be added just under the 
        //      first command.

        // Constructor
        // Do not pass the Guid into the constructor - the primary key property 
        public Student(string name, int year, string college)
        {
            this.Name = name;
            this.Year = year;
            this.College = college;
        }
    }
}