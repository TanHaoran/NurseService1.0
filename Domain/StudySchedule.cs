/**
  * Name:Train.Models-StudySchedule
  * Author: banshine
  * Description: StudySchedule 实体层 
  * Date: 2015-3-26 10:16:26
  * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aersysm.Domain
{
    public partial class StudySchedule
    {
        
            #region PlanID

            private String _PlanID;

            /// <summary>Gets or sets PlanID</summary>
            public String PlanID
            {
                get { return _PlanID; }
                set { _PlanID = value; }
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
        
        
            #region PlanName

            private String _PlanName;

            /// <summary>Gets or sets PlanName</summary>
            public String PlanName
            {
                get { return _PlanName; }
                set { _PlanName = value; }
            }

            #endregion
        
        
            #region StartDate

            private DateTime _StartDate;

            /// <summary>Gets or sets StartDate</summary>
            public DateTime StartDate
            {
                get { return _StartDate; }
                set { _StartDate = value; }
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
        
    }
    
}

