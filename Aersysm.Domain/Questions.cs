/**
  * Name:Train.Models-Questions
  * Author: banshine
  * Description: Questions 实体层 
  * Date: 2015-3-26 10:16:26
  * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aersysm.Domain
{
    public partial class Questions
    {
        
            #region Qid

            private String _Qid;

            /// <summary>Gets or sets Qid</summary>
            public String Qid
            {
                get { return _Qid; }
                set { _Qid = value; }
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
        
        
            #region TypeName

            private String _TypeName;

            /// <summary>Gets or sets TypeName</summary>
            public String TypeName
            {
                get { return _TypeName; }
                set { _TypeName = value; }
            }

            #endregion
        
        
            #region TitleName

            private String _TitleName;

            /// <summary>Gets or sets TitleName</summary>
            public String TitleName
            {
                get { return _TitleName; }
                set { _TitleName = value; }
            }

            #endregion
        
        
            #region SpellNo

            private String _SpellNo;

            /// <summary>Gets or sets SpellNo</summary>
            public String SpellNo
            {
                get { return _SpellNo; }
                set { _SpellNo = value; }
            }

            #endregion
        
        
            #region A

            private String _A;

            /// <summary>Gets or sets A</summary>
            public String A
            {
                get { return _A; }
                set { _A = value; }
            }

            #endregion
        
        
            #region B

            private String _B;

            /// <summary>Gets or sets B</summary>
            public String B
            {
                get { return _B; }
                set { _B = value; }
            }

            #endregion
        
        
            #region C

            private String _C;

            /// <summary>Gets or sets C</summary>
            public String C
            {
                get { return _C; }
                set { _C = value; }
            }

            #endregion
        
        
            #region D

            private String _D;

            /// <summary>Gets or sets D</summary>
            public String D
            {
                get { return _D; }
                set { _D = value; }
            }

            #endregion
        
        
            #region E

            private String _E;

            /// <summary>Gets or sets E</summary>
            public String E
            {
                get { return _E; }
                set { _E = value; }
            }

            #endregion
        
        
            #region F

            private String _F;

            /// <summary>Gets or sets F</summary>
            public String F
            {
                get { return _F; }
                set { _F = value; }
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
        
        
            #region Result

            private String _Result;

            /// <summary>Gets or sets Result</summary>
            public String Result
            {
                get { return _Result; }
                set { _Result = value; }
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
            
            //public 
            #endregion
        
    }
    
}

