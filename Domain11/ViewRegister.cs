﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Aersysm.Domain
{
    [DataContract]
    public partial class ViewRegister
    {

        [DataMember]
        public String Phone { get; set; }

        [DataMember]
        public String Password { get; set; }
    }
}
