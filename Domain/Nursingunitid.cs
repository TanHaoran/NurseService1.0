/***************************************************************************
 * 
 *       功能：     实体类
 *       作者：     YanMing
 *       日期：     2017/7/29 19:17:40
 * 
 *       修改日期： 
 *       修改人：
 *       修改内容：
 * 
 * *************************************************************************/
namespace Aersysm.Domain
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// nursingunitid 
    /// </summary>
    [Serializable]
    [DataContract]
    public partial class Nursingunitid
	{
		public Nursingunitid()
		{
			
		}
		private string _nursingunitid;
		private string _name;
		private string _address;
		private string _contact;
		private string _phone;
		private string _spellcode;
		private int _isflag;
		private string _introduction;
		private int _displayorder;
		
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string NursingUnitId
		{
			get{return _nursingunitid;}
			set{_nursingunitid = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string Name
		{
			get{return _name;}
			set{_name = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string Address
		{
			get{return _address;}
			set{_address = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string Contact
		{
			get{return _contact;}
			set{_contact = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string Phone
		{
			get{return _phone;}
			set{_phone = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string SpellCode
		{
			get{return _spellcode;}
			set{_spellcode = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public int IsFlag
		{
			get{return _isflag;}
			set{_isflag = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string Introduction
		{
			get{return _introduction;}
			set{_introduction = value;}
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
