using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // This is for the line telling the database how to generate values for new entries 
using System.Linq;
using System.Web;

namespace Day2Demo.Models
{
    public class Grade
    {
        // Class fields - private set to prevent them from changing after obect instantiation
        // This is regex
        [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,40}$", ErrorMessage = "No special characters allowed.")]
        public string CourseTitle { get; set; }
        // When specifying an ErrorMessage, the string cannot be empty; at least one character must be included.
        [Required(ErrorMessage = "Missing Course Result")]
        public string Passed { get; set; }

        // This property tells the database how to generate values for a new entry - in this case, it generates 
        //      a value each time a row is inserted.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // Guid = globally unique identifier: like the keys in a hashtable - all unique
        // This is an ideal type for a primary key property, as entries in a database must be unique.
        // In this case, Guid GradeIdentifier will serve as the primary key property for this model class.
        public Guid Id { get; private set; }
        // A note on the above field + command pair: VS automatically looks for a field named Id - if the primary key
        //      field does not follow the naming convention, an additional command [Key] must be added just under the 
        //      first command.

        // Colour of the text, to be represented in HTML code
        public string ResultColor { get; set; }

        // Default constructor
        public Grade()
        {
            // Basics!! If you do not specify a constructor, C# will create a default one for you.
            // HOWEVER, if you specify a parameterised constructor, C# will NOT create a default one for you if
            //      parameters are not passed in. Thus, you must create the default constructor manually.
        }

        // Parameterised constructor
        // Do not pass the Guid into the constructor - the primary key property 
        public Grade(string courseTitle, string passed)
        {
            this.CourseTitle = courseTitle;
            this.Passed = passed;
        }
    }
}