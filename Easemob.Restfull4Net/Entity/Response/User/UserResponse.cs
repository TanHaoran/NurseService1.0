﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easemob.Restfull4Net.Entity.Response
{
    public class UserResponse:BaseResponse
    {
        public string action { get; set; }
        public string application { get; set; }
        public UserParams @params { get; set; }
        public string path { get; set; }
        public string uri { get; set; }
        public UserEntity[] entities { get; set; }
        public long timestamp { get; set; }
        public int duration { get; set; }
        public string organization { get; set; }
        public string applicationName { get; set; }
        public string cursor { get; set; }
        public int count { get; set; }
        public Dictionary<string,object> data { get; set; }
    }

    public class UserParams
    {
        public string[] limit { get; set; }
        public string[] cursor { get;set; }
    }

    public class UserEntity
    {
        public string uuid { get; set; }
        public string type { get; set; }
        public long created { get; set; }
        public long modified { get; set; }
        public string username { get; set; }
        public bool activated { get; set; }
        public string device_token { get; set; }
        public string nickname { get; set; }
    }
}
