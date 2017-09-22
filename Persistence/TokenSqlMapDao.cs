using Aersysm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aersysm.Persistence {
    public partial class TokenSqlMapDao : BaseSqlMapDao {
        public TokenSqlMapDao() {
            //
            // TODO: 此处添加userauthsSqlMapDao的构造函数
            //
        }

        /// <summary>
        /// 得到列表
        /// </summary>
        public IList<Token> GetTokenList() {
            return ExecuteQueryForList<Token>("SelectToken", null);
        }

        /// <summary>
        /// 新建
        /// </summary>
        public void AddToken(Token token) {
            //int id = GetId("userauths");
            //userauths.AuthsId = id;

            ExecuteInsert("InsertToken", token);
        }
        /// <summary>
        /// 修改
        /// </summary>
        public void UpdateToken(Token token) {
            ExecuteUpdate("UpdateToken", token);
        }

        /// <summary>
        /// 得到明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Token GetToken(System.String RegisterId) {
            return ExecuteQueryForObject<Token>("SelectToken", RegisterId);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public void DeleteToken(System.String RegisterId) {
            ExecuteDelete("DeleteToken", RegisterId);
        }
        // DeleteUserauths

        public int DeleteAll() {
            return ExecSQL("delete from Token");
        }
    }
}
