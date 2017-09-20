/**
  * Name:Train.Models-Recharge
  * Author: banshine
  * Description: Recharge 实体层 
  * Date: 2015-3-26 10:16:26
  * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aersysm.Domain
{
    public partial class Recharge
    {
        
            #region UserID

            private String _UserID;

            /// <summary>Gets or sets UserID</summary>
            public String UserID
            {
                get { return _UserID; }
                set { _UserID = value; }
            }

            #endregion
        
        
            #region RechargeTime

            private DateTime _RechargeTime;

            /// <summary>Gets or sets RechargeTime</summary>
            public DateTime RechargeTime
            {
                get { return _RechargeTime; }
                set { _RechargeTime = value; }
            }

            #endregion
        
        
            #region Count

            private Int32 _Count;

            /// <summary>Gets or sets Count</summary>
            public Int32 Count
            {
                get { return _Count; }
                set { _Count = value; }
            }

            #endregion
        
    }
    
}

