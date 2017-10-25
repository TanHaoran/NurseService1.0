/***************************************************************************
 * 
 *       功能：     持久层基类
 *       作者：     YanMing
 *       日期：     2017/8/9 14:53:45
 * 
 *       修改日期： 
 *       修改人：
 *       修改内容：
 * 
 * *************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using IBatisNet.Common;
using IBatisNet.DataMapper;
using IBatisNet.Common.Exceptions;
using Aersysm.Domain;

namespace Aersysm.Persistence {
    /// <summary>
    /// emchatSqlMapDao
    /// </summary>
    public partial class EmchatSqlMapDao : BaseSqlMapDao {
        public EmchatSqlMapDao() {
            //
            // TODO: 此处添加emchatSqlMapDao的构造函数
            //
        }

        /// <summary>
        /// 得到列表
        /// </summary>
        public IList<Emchat> GetEmchatList() {
            return ExecuteQueryForList<Emchat>("SelectEmchat", null);
        }

        /// <summary>
        /// 新建
        /// </summary>
        public void Addemchat(Emchat emchat) {
            //int id = GetId("emchat");
            //emchat.EMChatId = id;

            ExecuteInsert("InsertEmchat", emchat);
        }
        /// <summary>
        /// 修改
        /// </summary>
        public void Updateemchat(Emchat emchat) {
            ExecuteUpdate("UpdateEmchat", emchat);
        }

        /// <summary>
        /// 得到明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Emchat GetEmchatDetail(System.String EMChatId) {
            return ExecuteQueryForObject<Emchat>("SelectEmchat", EMChatId);
        }

        /// <summary>
        ///  得到包含备注的好友信息
        /// </summary>
        /// <param name="EMchatId"></param>
        /// <param name="EMFriendId"></param>
        /// <returns></returns>
        public Emchat GetEmchatDetailRemark(string EMMyId, string EMFriendId) {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("EMMyId", EMMyId);
            dic.Add("EMFriendId", EMFriendId);
            return ExecuteQueryForObject<Emchat>("SelectEmchatRemark", dic);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public void Deleteemchat(System.String EMChatId) {
            ExecuteDelete("DeleteEmchat", EMChatId);
        }


    }
}
