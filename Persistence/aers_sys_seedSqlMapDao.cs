/**
  * Name:Aersysm.SqlMapDao-aers_sys_seed
  * Author: banshine
  * Description: aers_sys_seed Dao层 
  * Date: 2015-6-8 11:13:02
  * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aersysm.Domain;
using System.Collections;

namespace Aersysm.Persistence
{
    public partial class aers_sys_seedSqlMapDao: BaseSqlMapDao
    {
        public string GetMaxID(string TableName)
        {

            try
            {
                string result = "";
                String stmtId = "aers_sys_seed_Find";

                Hashtable ht = new Hashtable();
                ht.Add("TableName", TableName);
                aers_sys_seed seedEntity = ExecuteQueryForObject<aers_sys_seed>(stmtId, ht);

                if (seedEntity == null)
                {
                    seedEntity = new aers_sys_seed();
                    seedEntity.TableName = TableName;
                    seedEntity.MaxNo = 1;
                    seedEntity.Length = 10;
                    seedEntity.Prefix = "";
                    SeedInsert(seedEntity);
                    result = seedEntity.MaxNo.ToString().PadLeft(seedEntity.Length, '0');
                }
                else
                {
                    seedEntity.MaxNo++;
                    string prefix = string.Empty;
                    if (!string.IsNullOrEmpty(seedEntity.Prefix))
                    {
                        prefix = seedEntity.Prefix;
                    }
                    SeedUpdate(seedEntity);
                    result = prefix + seedEntity.MaxNo.ToString().PadLeft(seedEntity.Length - prefix.Length, '0');

                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void SeedInsert(aers_sys_seed seed)
        {
            String stmtId = "aers_sys_seed_Insert";
            ExecuteInsert(stmtId, seed);

        }


        public void SeedUpdate(aers_sys_seed seed)
        {
            String stmtId = "aers_sys_seed_Update";
            ExecuteUpdate(stmtId, seed);

        }

        public aers_sys_seed SeedFind(string TableName)
        {
            String stmtId = "aers_sys_seed_Find";

            Hashtable ht = new Hashtable();
            ht.Add("TableName", TableName);
            aers_sys_seed result = ExecuteQueryForObject<aers_sys_seed>(stmtId, ht);
            return result;
        }













    }
    
}

