/**
  * Name:Aersysm.Models-aers_sys_statecode
  * Author: banshine
  * Description: aers_sys_statecode 实体层 
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
    public partial class aers_sys_statecode
    {
        
            #region ECodeId
            
            [DataMember]
            public String ECodeId { get;set; }
            
            #endregion
        
        
            #region ECodeTag
            
            [DataMember]
            public String ECodeTag { get;set; }
            
            #endregion
        
        
            #region ECodeValue
            
            [DataMember]
            public String ECodeValue { get;set; }
            
            #endregion
        
        
            #region IsFlag
            
            [DataMember]
            public Int32 IsFlag { get;set; }
            
            #endregion
        
        
            #region Remarks
            
            [DataMember]
            public String Remarks { get;set; }


        [DataMember]
        public String Classify { get; set; }
        

        #endregion

    }
    
}

