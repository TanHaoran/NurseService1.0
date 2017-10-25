/**
  * Name:Train.Models-aers_tbl_basicsdep
  * Author: banshine
  * Description: aers_tbl_basicsdep 实体层 
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
    public partial class aers_tbl_basicsdep
    {
        
            #region BasdepId
            
            [DataMember]
            public String BasdepId { get;set; }
            
            #endregion
        
        
            #region BasdeptName
            
            [DataMember]
            public String BasdeptName { get;set; }
            
            #endregion
        
        
            #region SpellNo
            
            [DataMember]
            public String SpellNo { get;set; }
            
            #endregion
        
        
            #region DisplayOrder
            
            [DataMember]
            public Int32 DisplayOrder { get;set; }
            
            #endregion
        
        
            #region Remarks
            
            [DataMember]
            public String Remarks { get;set; }
            
            #endregion
        
        
            #region IsFlag
            
            [DataMember]
            public Int32 IsFlag { get;set; }
            
            #endregion
        
        
            #region OperatorId
            
            [DataMember]
            public String OperatorId { get;set; }
            
            #endregion
        
        
            #region OperatorDate
            
            [DataMember]
            public DateTime OperatorDate { get;set; }

        #endregion


        #region   Basdeplogo
        [DataMember]
        public String Basdeplogo { get; set; }

        #endregion

    }

}

