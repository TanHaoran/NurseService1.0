/**
  * Name:Train.Models-aers_tbl_hospital
  * Author: banshine
  * Description: aers_tbl_hospital 实体层 
  * Date: 2015-6-8 10:57:02
  * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Aersysm.Domain
{
    [DataContract]
    public partial class aers_tbl_hospital
    {
        
            #region HospId
            
            [DataMember]
            public String HospId { get;set; }
            
            #endregion
        
        
            #region HospName
            
            [DataMember]
            public String HospName { get;set; }
            
            #endregion
        
        
            #region Address
            
            [DataMember]
            public String Address { get;set; }
            
            #endregion
        
        
            #region Phone
            
            [DataMember]
            public String Phone { get;set; }
            
            #endregion
        
        
            #region Contact
            
            [DataMember]
            public String Contact { get;set; }
            
            #endregion
        
        
            #region Grade
            
            [DataMember]
            public String Grade { get;set; }
            
            #endregion
        
        
            #region Validitytime
            
            [DataMember]
            public DateTime Validitytime { get;set; }
            
            #endregion
        
        
            #region IsFlag
            
            [DataMember]
            public Int32 IsFlag { get;set; }
            
            #endregion
        
        
            #region Remarks
            
            [DataMember]
            public String Remarks { get;set; }
            
            #endregion
        
        
            #region OperatorId
            
            [DataMember]
            public String OperatorId { get;set; }
            
            #endregion
        
        
            #region OperatorDate
            
            [DataMember]
            public DateTime OperatorDate { get;set; }

        #endregion

        [DataMember]
        public String Hosplogo { get; set; }



        [DataMember]
        public String HospUnion { get; set; }



        [DataMember]
        public Int32 EveCount { get; set; }


        }
    
}

