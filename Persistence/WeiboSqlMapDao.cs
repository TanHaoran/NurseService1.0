using Aersysm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aersysm.Persistence
{
   public  class WeiboSqlMapDao : BaseSqlMapDao
    {
        public WeiboSqlMapDao()
        {
            //
            // TODO: 此处添加qqSqlMapDao的构造函数
            //
        }

        /// <summary>
        /// 得到列表
        /// </summary>
        public IList<Weibo> GetWeiboList()
        {
            return ExecuteQueryForList<Weibo>("SelectWeibo", null);
        }

        /// <summary>
        /// 新建
        /// </summary>
        public void AddWeibo(Weibo weibo)
        {

            ExecuteInsert("InsertWeibo", weibo);
        }
        /// <summary>
        /// 修改
        /// </summary>
        public void UpdateWeibo(Weibo weibo)
        {
            ExecuteUpdate("UpdateWeibo", weibo);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public void DeleteWeibo(System.String Id)
        {
            ExecuteDelete("DeleteWeibo", Id);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public void DeleteWeiboByOpenId(System.String OpenId)
        {
            ExecuteDelete("DeleteWeiboByOpenId", OpenId);
        }
    }
}
