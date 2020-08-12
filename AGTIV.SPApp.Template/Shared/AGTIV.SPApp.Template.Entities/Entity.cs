using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace AGTIV.SPApp.Template.Entities
{
    [Serializable]
    [DataContract]
    public class Entity
    {
        [DataMember]
        public string spHostURL { get; set; }

        [DataMember]
        public string accessToken { get; set; }
    }
}
