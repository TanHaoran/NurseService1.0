using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Aersysm.Domain
{
    [Serializable]
    [DataContract]
    public partial  class NurseBaseInfo
    {
        public string RegisterId { get; set; }

        public string Phone { get; set; }

        public string  Name { get; set; }

        public string  Sex { get; set; }

        public string  DepartmentId { get; set; }

        public string  Role { get; set; }  //职务?

        public string  QQNickName { get; set; }

        public string  WXNickName { get; set; }

        public string  WBNickName { get; set; }

        public string BLSJRegisterId { get; set; }

        public string XFRegisterId { get; set; }
    }
}
