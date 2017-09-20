/**
  * Name:Train.Models-TrainDetailRecord
  * Author: banshine
  * Description: TrainDetailRecord 实体层 
  * Date: 2015-3-26 10:16:26
  * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aersysm.Domain
{
    public partial class TrainDetailRecord
    {
        
            #region DetailID

            private String _DetailID;

            /// <summary>Gets or sets DetailID</summary>
            public String DetailID
            {
                get { return _DetailID; }
                set { _DetailID = value; }
            }

            #endregion
        
        
            #region RecrodID

            private String _RecrodID;

            /// <summary>Gets or sets RecrodID</summary>
            public String RecrodID
            {
                get { return _RecrodID; }
                set { _RecrodID = value; }
            }

            #endregion
        
        
            #region ChapterID

            private String _ChapterID;

            /// <summary>Gets or sets ChapterID</summary>
            public String ChapterID
            {
                get { return _ChapterID; }
                set { _ChapterID = value; }
            }

            #endregion
        
        
            #region StandScore

            private Int32 _StandScore;

            /// <summary>Gets or sets StandScore</summary>
            public Int32 StandScore
            {
                get { return _StandScore; }
                set { _StandScore = value; }
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
        
        
            #region Points

            private Int32 _Points;

            /// <summary>Gets or sets Points</summary>
            public Int32 Points
            {
                get { return _Points; }
                set { _Points = value; }
            }

            #endregion
        
        
            #region LearnDate

            private DateTime _LearnDate;

            /// <summary>Gets or sets LearnDate</summary>
            public DateTime LearnDate
            {
                get { return _LearnDate; }
                set { _LearnDate = value; }
            }

            #endregion
        
    }
    
}

