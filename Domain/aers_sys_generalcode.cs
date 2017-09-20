/**
  * Name:Train.Models-aers_sys_generalcode
  * Author: banshine
  * Description: aers_sys_generalcode 实体层 
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
    public partial class aers_sys_generalcode
    {
        
            #region DictionaryID
            
            [DataMember]
            public String DictionaryID { get;set; }
            
            #endregion
        
        
            #region Content
            
            [DataMember]
            public String Content { get;set; }
            
            #endregion
        
        
            #region DisplayOrder
            
            [DataMember]
            public Int32 DisplayOrder { get;set; }
            
            #endregion
        
        
            #region Code
            
            [DataMember]
            public String Code { get;set; }
            
            #endregion
        
        
            #region SpellNo
            
            [DataMember]
            public String SpellNo { get;set; }
            
            #endregion
        
        
            #region ClassType
            
            [DataMember]
            public String ClassType { get;set; }
            
            #endregion
        
        
            #region Remark
            
            [DataMember]
            public String Remark { get;set; }
            
            #endregion
        
        
            #region IsFlag
            
            [DataMember]
            public Int32 IsFlag { get;set; }
            
            #endregion
        
        
            #region OperatorDate
            
            [DataMember]
            public DateTime OperatorDate { get;set; }
            
            #endregion
        
        
            #region OperatorID
            
            [DataMember]
            public String OperatorID { get;set; }
            
            #endregion
        
    }
    
}

