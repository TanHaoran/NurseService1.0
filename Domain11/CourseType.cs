/**
  * Name:Train.Models-CourseType
  * Author: banshine
  * Description: CourseType 实体层 
  * Date: 2015-3-26 10:16:26
  * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aersysm.Domain
{
    public partial class CourseType
    {
        
            #region TypeID

            private String _TypeID;

            /// <summary>Gets or sets TypeID</summary>
            public String TypeID
            {
                get { return _TypeID; }
                set { _TypeID = value; }
            }

            #endregion
        
        
            #region ParendID

            private String _ParendID;

            /// <summary>Gets or sets ParendID</summary>
            public String ParendID
            {
                get { return _ParendID; }
                set { _ParendID = value; }
            }

            #endregion


            #region TypeName

            private String _TypeName;

            /// <summary>Gets or sets CourseType</summary>
            public String TypeName
            {
                get { return _TypeName; }
                set { _TypeName = value; }
            }

            #endregion
        
        
            #region IndexNo

            private String _IndexNo;

            /// <summary>Gets or sets IndexNo</summary>
            public String IndexNo
            {
                get { return _IndexNo; }
                set { _IndexNo = value; }
            }

            #endregion
        
        
            #region Commentary

            private String _Commentary;

            /// <summary>Gets or sets Commentary</summary>
            public String Commentary
            {
                get { return _Commentary; }
                set { _Commentary = value; }
            }

            #endregion
        
        
            #region DisplayOrder

            private Int32 _DisplayOrder;

            /// <summary>Gets or sets DisplayOrder</summary>
            public Int32 DisplayOrder
            {
                get { return _DisplayOrder; }
                set { _DisplayOrder = value; }
            }

            #endregion
        
        
            #region IsEndLevel

            private Int32 _IsEndLevel;

            /// <summary>Gets or sets IsEndLevel</summary>
            public Int32 IsEndLevel
            {
                get { return _IsEndLevel; }
                set { _IsEndLevel = value; }
            }

            #endregion
        
        
            #region ModifyDate

            private DateTime _ModifyDate;

            /// <summary>Gets or sets ModifyDate</summary>
            public DateTime ModifyDate
            {
                get { return _ModifyDate; }
                set { _ModifyDate = value; }
            }

            #endregion
        
        
            #region OperatorID

            private String _OperatorID;

            /// <summary>Gets or sets OperatorID</summary>
            public String OperatorID
            {
                get { return _OperatorID; }
                set { _OperatorID = value; }
            }

            #endregion
        
    }
    
}

