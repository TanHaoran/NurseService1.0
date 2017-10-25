/**
  * Name:Train.Models-CourseCatalog
  * Author: banshine
  * Description: CourseCatalog 实体层 
  * Date: 2015-3-26 10:16:26
  * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aersysm.Domain
{
    public partial class CourseCatalog
    {
        
            #region CatalogID

            private String _CatalogID;

            /// <summary>Gets or sets CatalogID</summary>
            public String CatalogID
            {
                get { return _CatalogID; }
                set { _CatalogID = value; }
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
        
        
            #region CatalogName

            private String _CatalogName;

            /// <summary>Gets or sets CatalogName</summary>
            public String CatalogName
            {
                get { return _CatalogName; }
                set { _CatalogName = value; }
            }

            #endregion
        
        
            #region CatalogSort

            private Int32 _CatalogSort;

            /// <summary>Gets or sets CatalogSort</summary>
            public Int32 CatalogSort
            {
                get { return _CatalogSort; }
                set { _CatalogSort = value; }
            }

            #endregion
        
    }
    
}

