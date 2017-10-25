/**
  * Name:Train.Models-TrainRecord
  * Author: banshine
  * Description: TrainRecord 实体层 
  * Date: 2015-3-26 10:16:26
  * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aersysm.Domain
{
    public partial class TrainRecord
    {
        
            #region RecordID

            private String _RecordID;

            /// <summary>Gets or sets RecordID</summary>
            public String RecordID
            {
                get { return _RecordID; }
                set { _RecordID = value; }
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
        
        
            #region Score

            private Int32 _Score;

            /// <summary>Gets or sets Score</summary>
            public Int32 Score
            {
                get { return _Score; }
                set { _Score = value; }
            }

            #endregion
        
        
            #region Credit

            private Decimal _Credit;

            /// <summary>Gets or sets Credit</summary>
            public Decimal Credit
            {
                get { return _Credit; }
                set { _Credit = value; }
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
        
        
            #region HID

            private String _HID;

            /// <summary>Gets or sets HID</summary>
            public String HID
            {
                get { return _HID; }
                set { _HID = value; }
            }

            #endregion
        
        
            #region StartTime

            private DateTime _StartTime;

            /// <summary>Gets or sets StartTime</summary>
            public DateTime StartTime
            {
                get { return _StartTime; }
                set { _StartTime = value; }
            }

            #endregion
        
        
            #region EndDate

            private DateTime _EndDate;

            /// <summary>Gets or sets EndDate</summary>
            public DateTime EndDate
            {
                get { return _EndDate; }
                set { _EndDate = value; }
            }

            #endregion
        
        
            #region AuditStatus

            private Int32 _AuditStatus;

            /// <summary>Gets or sets AuditStatus</summary>
            public Int32 AuditStatus
            {
                get { return _AuditStatus; }
                set { _AuditStatus = value; }
            }

            #endregion
        
        
            #region Auditor

            private String _Auditor;

            /// <summary>Gets or sets Auditor</summary>
            public String Auditor
            {
                get { return _Auditor; }
                set { _Auditor = value; }
            }

            #endregion
        
    }
    
}

