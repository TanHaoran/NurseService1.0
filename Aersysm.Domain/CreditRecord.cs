/**
  * Name:Train.Models-CreditRecord
  * Author: banshine
  * Description: CreditRecord 实体层 
  * Date: 2015-3-26 10:16:26
  * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aersysm.Domain
{
    public partial class CreditRecord {
        
            #region RecordID

            private String _RecordID;

            /// <summary>Gets or sets RecordID</summary>
            public String RecordID
            {
                get { return _RecordID; }
                set { _RecordID = value; }
            }

            #endregion
        
        
            #region UserID

            private String _UserID;

            /// <summary>Gets or sets UserID</summary>
            public String UserID
            {
                get { return _UserID; }
                set { _UserID = value; }
            }

            #endregion
        
        
            #region TrainingID

            private String _TrainingID;

            /// <summary>Gets or sets TrainingID</summary>
            public String TrainingID
            {
                get { return _TrainingID; }
                set { _TrainingID = value; }
            }

            #endregion
        
        
            #region Grade

            private String _Grade;

            /// <summary>Gets or sets Grade</summary>
            public String Grade
            {
                get { return _Grade; }
                set { _Grade = value; }
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
        
        
            #region ModifyDate

            private DateTime _ModifyDate;

            /// <summary>Gets or sets ModifyDate</summary>
            public DateTime ModifyDate
            {
                get { return _ModifyDate; }
                set { _ModifyDate = value; }
            }

            #endregion
        
        
            #region HID

            private String _HID;

            /// <summary>Gets or sets HID</summary>
            public String HID
            {
                get { return _HID; }
                set { _HID = value; }
            }

            #endregion
        
    }
    
}

