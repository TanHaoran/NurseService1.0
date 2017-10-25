/**
  * Name:Train.Models-aers_sys_seed
  * Author: banshine
  * Description: aers_sys_seed 实体层 
  * Date: 2015-6-8 10:57:02
  * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Aersysm.Domain
{
    public partial class aers_sys_seed
    {
        
            #region TableName
            
            public String TableName { get;set; }
            
            #endregion
        
        
            #region MaxNo
            
            public Int32 MaxNo { get;set; }
            
            #endregion
        
        
            #region Length
            
            public Int32 Length { get;set; }
            
            #endregion
        
        
            #region Prefix
            
            public String Prefix { get;set; }
            
            #endregion
        
    }



    public partial class Region
    {

     

        public String RegionName { get; set; }

     

        public String RegionID { get; set; }

  

    }



    public partial class Hospitallevel
    {



        public String levelName { get; set; }



        public String levelID { get; set; }



    }

}

