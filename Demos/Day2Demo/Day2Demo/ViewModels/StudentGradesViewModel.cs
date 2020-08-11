using Day2Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace Day2Demo.ViewModel
{
    public class StudentGradesViewModel
    {
        // The view model is an intermediary structure that sits between the Model and View in the MVC architecture.
        // It acts as a data container for Views, containing one or more models, and serving to eliminate the need for 
        //      logic in the View.
        // Here, StudentViewModel contains the models Student and Grade.

        // Class fields
        // Public set to allow Student to be modified in the controller
        public Student Student { get; set; }

        public List<Grade> GradeBook { get; set; }

        public Grade Grade { get; set; }

        // Constructor
        public StudentGradesViewModel()
        {
            Student = null;
            GradeBook = new List<Grade>();
        }
    }
}