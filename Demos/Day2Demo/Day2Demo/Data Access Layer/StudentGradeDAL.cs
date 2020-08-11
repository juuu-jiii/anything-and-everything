using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity; // Probably using the installed NuGet package Entity Framework
using Day2Demo.Models;

namespace Day2Demo.Data_Access_Layer
{
    // Inheriting from DbContext gives the class the capability of querying from and writing to a database.
    public class StudentGradeDAL : DbContext
    {
        // DbSet is a collection of entities (of the same type) stored to and queried from the database.
        public DbSet<Grade> GradeBook { get; set; }
        //public DbSet<Student> Student { get; set; }
    }
}