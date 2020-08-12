using SzunKSharePointAddInWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace SzunKSharePointAddInWeb.ViewModels
{
    public class GradesViewModel
    {
        public Grade Grade { get; set; }
        // Required. Because you are dealing with multiple collections
        // Maybe create a list of grades called gradebook again
        public List<Grade> GradeBook { get; set; }

        // Constructor
        public GradesViewModel()
        {
            Grade = new Grade();
            GradeBook = new List<Grade>();
        }
    }
}