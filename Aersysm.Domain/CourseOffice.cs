/**
  * Name:Train.Models-CourseOffice
  * Author: banshine
  * Description: CourseOffice 实体层 
  * Date: 2015-3-26 10:16:26
  * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aersysm.Domain
{
    public partial class CourseOffice
    {
        
            #region Id

            private String _Id;

            /// <summary>Gets or sets Id</summary>
            public String Id
            {
                get { return _Id; }
                set { _Id = value; }
            }

            #endregion
        
        
            #region CourseID

            private String _CourseID;

            /// <summary>Gets or sets CourseID</summary>
            public String CourseID
            {
                get { return _CourseID; }
                set { _CourseID = value; }
            }

            #endregion
        
        
            #region OfficeID

            private String _OfficeID;

            /// <summary>Gets or sets OfficeID</summary>
            public String OfficeID
            {
                get { return _OfficeID; }
                set { _OfficeID = value; }
            }

            #endregion
        
    }
    
}

