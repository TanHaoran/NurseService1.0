using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aersysm.Domain
{

    public partial class view_tbl_registereduser
    {
   
        /// <summary>
        /// 调用服务验证码
        /// </summary>
        public string LoginKey { get; set; }



        #region 注册用户编码
        /// <summary>
        /// 注册用户编码
        /// </summary>
        public string ReUId { get; set; }
        #endregion

        #region 用户姓名
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }
        #endregion

        #region 用户组
        /// <summary>
        /// 用户组
        /// </summary>
        public string GroupRole { get; set; }
        #endregion

        #region 所在科室ID
        /// <summary>
        /// 用户组
        /// </summary>
        public string HospDepId { get; set; }
        #endregion

        #region 所在医院ID
        /// <summary>
        /// 用户组
        /// </summary>
        public string HospId { get; set; }
        #endregion

        public String RoleState { get; set; }



        public int IsFlag { get; set; }

        public string Address { get; set; }


        public DateTime SystemTime { get; set; }


    }
}
