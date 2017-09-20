using Aersysm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aersysm.Persistence
{
   public  class WeixinSqlMapDao : BaseSqlMapDao
    {
        public WeixinSqlMapDao()
        {
            //
            // TODO: 此处添加qqSqlMapDao的构造函数
            //
        }

        /// <summary>
        /// 得到列表
        /// </summary>
        public IList<Weixin> GetWeixinList()
        {
            return ExecuteQueryForList<Weixin>("SelectWeixin", null);
        }

        /// <summary>
        /// 新建
        /// </summary>
        public void AddWeixin(Weixin weixin)
        {

            ExecuteInsert("InsertWeixin", weixin);
        }
        /// <summary>
        /// 修改
        /// </summary>
        public void UpdateWeixin(Weixin weixin)
        {
            ExecuteUpdate("UpdateWeixin", weixin);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public void DeleteWeixin(System.String Id)
        {
            ExecuteDelete("DeleteWeixin", Id);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public void DeleteWeixinByOpenId(System.String OpenId)
        {
            ExecuteDelete("DeleteWeixinByOpenId", OpenId);
        }
    }
}
