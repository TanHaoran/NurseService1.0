/**
  * Name:Train.Models-CourseSection
  * Author: banshine
  * Description: CourseSection 实体层 
  * Date: 2015-3-26 10:16:26
  * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aersysm.Domain
{
    public partial class CourseSection
    {
        
            #region ChapterID

            private String _ChapterID;

            /// <summary>Gets or sets ChapterID</summary>
            public String ChapterID
            {
                get { return _ChapterID; }
                set { _ChapterID = value; }
            }

        #endregion


        #region _CatalogID

        private String _CatalogID;

            /// <summary>Gets or sets ParentID</summary>
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
        
        
            #region ChapterName

            private String _ChapterName;

            /// <summary>Gets or sets ChapterName</summary>
            public String ChapterName
            {
                get { return _ChapterName; }
                set { _ChapterName = value; }
            }

            #endregion
        
        
            #region ChapterDuration

            private DateTime _ChapterDuration;

            /// <summary>Gets or sets ChapterDuration</summary>
            public DateTime ChapterDuration
            {
                get { return _ChapterDuration; }
                set { _ChapterDuration = value; }
            }

            #endregion
        
        
            #region ChapterType

            private Int32 _ChapterType;

            /// <summary>Gets or sets ChapterType</summary>
            public Int32 ChapterType
            {
                get { return _ChapterType; }
                set { _ChapterType = value; }
            }

            #endregion
        
        
            #region AuthorizationType

            private Int32 _AuthorizationType;

            /// <summary>Gets or sets AuthorizationType</summary>
            public Int32 AuthorizationType
            {
                get { return _AuthorizationType; }
                set { _AuthorizationType = value; }
            }

            #endregion
        
        
            #region ChapterSort

            private Int32 _ChapterSort;

            /// <summary>Gets or sets ChapterSort</summary>
            public Int32 ChapterSort
            {
                get { return _ChapterSort; }
                set { _ChapterSort = value; }
            }

            #endregion
        
        
            #region ContentUrl

            private String _ContentUrl;

            /// <summary>Gets or sets ContentUrl</summary>
            public String ContentUrl
            {
                get { return _ContentUrl; }
                set { _ContentUrl = value; }
            }

            #endregion
        
        
            #region ThumbUrl

            private String _ThumbUrl;

            /// <summary>Gets or sets ThumbUrl</summary>
            public String ThumbUrl
            {
                get { return _ThumbUrl; }
                set { _ThumbUrl = value; }
            }

            #endregion
        
        
            #region ChapterPoints

            private Int32 _ChapterPoints;

            /// <summary>Gets or sets ChapterPoints</summary>
            public Int32 ChapterPoints
            {
                get { return _ChapterPoints; }
                set { _ChapterPoints = value; }
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
        
        
            #region OperatorID

            private String _OperatorID;

            /// <summary>Gets or sets OperatorID</summary>
            public String OperatorID
            {
                get { return _OperatorID; }
                set { _OperatorID = value; }
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
        
    }
    
}

