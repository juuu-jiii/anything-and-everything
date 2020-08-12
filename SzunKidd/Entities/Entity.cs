using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entities
{
    [Serializable]
    [DataContract]
    public class Entity
    {
        [DataMember]
        public string SpHostURL { get; set; }

        [DataMember]
        public string AccessToken { get; set; }

    }
}
