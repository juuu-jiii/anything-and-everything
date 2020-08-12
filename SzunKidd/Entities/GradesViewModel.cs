using SzunKiddWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace Entities
{
    [Serializable]
    [DataContract]
    public class GradesViewModel : Entity
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