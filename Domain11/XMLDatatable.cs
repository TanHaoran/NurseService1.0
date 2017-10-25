using System;
using System.Runtime.Serialization;


namespace Aersysm.Domain
{
    [Serializable]
    [DataContract]
    public  class XMLDatatable
    {
        [DataMember]
        public string  Name { get; set; }

        [DataMember]
        public string HospitalId { get; set; }
        //[DataMember]
        //public string Address { get; set; }
    }
}
