using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Aersysm.Domain
{
    [DataContract]
    public  class ViewResetPassword
    {
        [DataMember]
        public String RegisterId { get; set; }

        [DataMember]
        public String PasswordNew { get; set; }

        [DataMember]
        public String PasswordOld { get; set; }

        //[DataMember]
        //public int Type { get; set; }   //修改密码，院内账号0，自己注册账号1
    }
}
