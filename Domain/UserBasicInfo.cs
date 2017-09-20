
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Aersysm.Domain
{

    [Serializable]
    [DataContract]
    public class UserBasicInfo
    {

        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string RegisterId { get; set; }

        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string Sex { get; set; }

        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string IDCard { get; set; }
        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
      //  [JsonConverter(typeof(JavaScriptDateTimeConverter))]
        public DateTime Birthday { get; set; }
       
      
        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public int Age { get; set; }

        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string Province { get; set; }

        ///<sumary>
        /// 
        ///</sumary>[DataMember]
        [DataMember]
        public string City { get; set; }

        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string Region { get; set; }

        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string Address { get; set; }

        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string Nation { get; set; }

        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string MeritalStatus { get; set; }

        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string Education { get; set; }

        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string QQ { get; set; }

        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string EMail { get; set; }

    }
}



