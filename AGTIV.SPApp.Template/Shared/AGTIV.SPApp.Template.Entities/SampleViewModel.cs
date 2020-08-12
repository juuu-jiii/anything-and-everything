using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.SPApp.Template.Entities
{
    [Serializable]
    [DataContract]
    public class SampleViewModel :Entity
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Description { get; set; }
    }

    [Serializable]
    [DataContract]
    public class SampleTokenModel : Entity
    {
        [DataMember]
        public string Name { get; set; }
    }
}
