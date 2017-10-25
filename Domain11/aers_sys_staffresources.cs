/**
  * Name:Train.Models-aers_sys_staffresources
  * Author: banshine
  * Description: aers_sys_staffresources 实体层 
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
    public partial class aers_sys_staffresources
    {
        
            #region ResId
            
            public String ResId { get;set; }
            
            #endregion
        
        
            #region ResName
            
            [DataMember]
            public String ResName { get;set; }
            
            #endregion
        
        
            #region ResUri
            
            [DataMember]
            public String ResUri { get;set; }
            
            #endregion
        
        
            #region ParentId
            
            [DataMember]
            public String ParentId { get;set; }
            
            #endregion
        
        
            #region StaffId
            
            [DataMember]
            public String StaffId { get;set; }
            
            #endregion
        
        
            #region Remark
            
            [DataMember]
            public String Remark { get;set; }
            
            #endregion
        
        
            #region IsFlag
            
            public Int32 IsFlag { get;set; }
            
            #endregion
        
        
            #region OperatorDate
            
            public DateTime OperatorDate { get;set; }
            
            #endregion
        
        
            #region OperatorID
            
            public String OperatorID { get;set; }
            
            #endregion
        
    }
    
}

