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
        // These fields must match those in the API
        [DataMember]
        public Grade Grade { get; set; }

        [DataMember]
        // Required. Because you are dealing with multiple collections
        // Maybe create a list of grades called gradebook again
        public List<Grade> GradeBook { get; set; }

        //// Constructor
        //public GradesViewModel()
        //{
        //    Grade = new Grade();
        //    GradeBook = new List<Grade>();
        //}

        [Serializable]
        [DataContract]
        public class TokenModel : Entity
        {
            [DataMember]
            public string Name { get; set; }
        }
    }
}