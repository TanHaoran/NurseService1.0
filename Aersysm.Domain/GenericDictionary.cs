/**
  * Name:Train.Models-GenericDictionary
  * Author: banshine
  * Description: GenericDictionary 实体层 
  * Date: 2015-3-26 10:16:26
  * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aersysm.Domain
{
    public partial class GenericDictionary
    {
        
            #region DictionaryID

            private String _DictionaryID;

            /// <summary>Gets or sets DictionaryID</summary>
            public String DictionaryID
            {
                get { return _DictionaryID; }
                set { _DictionaryID = value; }
            }

            #endregion
        
        
            #region Content

            private String _Content;

            /// <summary>Gets or sets Content</summary>
            public String Content
            {
                get { return _Content; }
                set { _Content = value; }
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
        
        
            #region Code

            private String _Code;

            /// <summary>Gets or sets Code</summary>
            public String Code
            {
                get { return _Code; }
                set { _Code = value; }
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
        
        
            #region ClassType

            private String _ClassType;

            /// <summary>Gets or sets ClassType</summary>
            public String ClassType
            {
                get { return _ClassType; }
                set { _ClassType = value; }
            }

            #endregion
        
        
            #region Remark

            private String _Remark;

            /// <summary>Gets or sets Remark</summary>
            public String Remark
            {
                get { return _Remark; }
                set { _Remark = value; }
            }

            #endregion
        
        
            #region OperatorTime

            private DateTime _OperatorTime;

            /// <summary>Gets or sets OperatorTime</summary>
            public DateTime OperatorTime
            {
                get { return _OperatorTime; }
                set { _OperatorTime = value; }
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

