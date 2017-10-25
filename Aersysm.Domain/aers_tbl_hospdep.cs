/**
  * Name:Train.Models-aers_tbl_hospdep
  * Author: banshine
  * Description: aers_tbl_hospdep 实体层 
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
    public partial class aers_tbl_hospdep
    {
        
            #region HospdepId
            
            [DataMember]
            public String HospdepId { get;set; }
            
            #endregion
        
        
            #region BasedepId
            
            [DataMember]
            public String BasedepId { get;set; }
            
            #endregion
        
        
            #region HospId
            
            [DataMember]
            public String HospId { get;set; }
            
            #endregion
        
        
            #region HospdepName
            
            [DataMember]
            public String HospdepName { get;set; }
            
            #endregion
        
        
            #region SpellNo
            
            [DataMember]
            public String SpellNo { get;set; }
            
            #endregion
        
        
            #region HospdepLogo
            
            [DataMember]
            public String HospdepLogo { get;set; }
            
            #endregion
        
        
            #region DisplayOrder
            
            [DataMember]
            public Int32 DisplayOrder { get;set; }
            
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
        public String HospName { get; set; }
        [DataMember]
        public String Grade { get; set; }

        [DataMember]
        public int EveCount { get; set; }

    }

}

