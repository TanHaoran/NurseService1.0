/***************************************************************************
 * 
 *       功能：     实体类
 *       作者：     BuzzlySoft
 *       日期：     2017-07-26
 * 
 *       修改日期： 
 *       修改人：
 *       修改内容：
 * 
 * *************************************************************************/
using System;
using System.Runtime.Serialization;

namespace Aersysm.Domain
{
	
	/// <summary>
	/// country 
	/// </summary>
	[Serializable]
    [DataContract]
	public class country
	{
		public country()
		{
            //string dd;
            //dd.Substring (

        }
		
		private string _countryid;
		private string _countryname;
		private string _countrycode;
		private string  _isflag;
		private int _displayorder;
		
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string CountryId
		{
			get{return _countryid;}
			set{_countryid = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string CountryName
		{
			get{return _countryname;}
			set{_countryname = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string CountryCode
		{
			get{return _countrycode;}
			set{_countrycode = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string  IsFlag
		{
			get{return _isflag;}
			set{_isflag = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public int DisplayOrder
		{
			get{return _displayorder;}
			set{_displayorder = value;}
		}


    }
}
