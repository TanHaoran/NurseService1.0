/**
  * Name:Train.Models-aers_tbl_staff
  * Author: banshine
  * Description: aers_tbl_staff 实体层 
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
    public partial class aers_tbl_staff
    {

        

        [DataMember]
        public String HospAddress { get; set; }
   


        [DataMember]
        public String HospName { get; set; }


        [DataMember]
        public String LoginName { get; set; }

        [DataMember]
        public String pwd { get; set; }

        [DataMember]
            public String StaffId { get;set; }
            
   
        
        
     
            
            [DataMember]
            public String ReguserId { get;set; }
            
           
            
            [DataMember]
            public String DepId { get;set; }
            
           
            
            [DataMember]
            public String Name { get;set; }
            
           
            
            [DataMember]
            public String RoleState { get;set; }
            
            
            
            [DataMember]
            public String Sex { get;set; }
            
            
            
            [DataMember]
            public String Position { get;set; }
            
            
            
            [DataMember]
            public String Phone { get;set; }
            
            
            
            [DataMember]
            public String Address { get;set; }
            
            
            
            [DataMember]
            public String IDNumber { get;set; }
            
           
            
            [DataMember]
            public Int32 IsFlag { get;set; }
            
           
            
            [DataMember]
            public String Remarks { get;set; }
            
            
            
            [DataMember]
            public String OperatorId { get;set; }
            
           
            
            [DataMember]
            public DateTime OperatorDate { get;set; }


            [DataMember]
            public String StringRoleState { get; set; }


            [DataMember]
            public String HospId { get; set; }


            [DataMember]
            public String HospdepName { get; set; }

           [DataMember]
           public String HeadImg { get; set; }
       
         

    }
    
}

