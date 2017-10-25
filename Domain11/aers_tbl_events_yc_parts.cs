/**
  * Name:Aersysm.Models-aers_tbl_events_yc_parts
  * Author: banshine
  * Description: aers_tbl_events_yc_parts 实体层 
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
    public partial class aers_tbl_events_yc_parts
    {

        #region PartsId
        [DataMember]
        public String PartsId { get;set; }

        #endregion


        #region EveycId
        [DataMember]
        public String EveycId { get;set; }
            
            #endregion
        
        
            #region PartsName
            
            [DataMember]
            public String PartsName { get;set; }
            
            #endregion
        
        
            #region MeterLength
            
            [DataMember]
            public String MeterLength { get;set; }
            
            #endregion
        
        
            #region MeterWidth
            
            [DataMember]
            public String MeterWidth { get;set; }
            
            #endregion
        
        
            #region MeterHeight
            
            [DataMember]
            public String MeterHeight { get;set; }
            
            #endregion
        
        
            #region QhPoint
            
            [DataMember]
            public String QhPoint { get;set; }
            
            #endregion
        
        
            #region QhDepth
            
            [DataMember]
            public String QhDepth { get;set; }
            
            #endregion
        
        
            #region Staging
            
            [DataMember]
            public String Staging { get;set; }
            
            #endregion
        
        
            #region DisplayOrder
            
            [DataMember]
            public String DisplayOrder { get;set; }
            
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


            #region EveresName

            [DataMember]
            public String EveresName { get; set; }

            #endregion


            #region DamageName 

            [DataMember]
            public String DamageName{ get; set; }

            #endregion


            #region DamageSite  

            [DataMember]
            public String DamageSite { get; set; }

            #endregion


            #region DamageArea  

            [DataMember]
            public String DamageArea { get; set; }

            #endregion

            #region DamageRemark  

            [DataMember]
            public String DamageRemark { get; set; }

            #endregion
    }

}

