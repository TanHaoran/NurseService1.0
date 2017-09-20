using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;
using IBatisNet.DataMapper.SessionStore;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Aersysm.Untility.IBatisNet
{
    /// <summary>
    /// 提供 ISqlMapper 对象，属于单件模式（Singleton Pattern）
    /// </summary>
    public class SqlMapperHelper
    {

        #region 私有变量
        /// <summary>
        /// ISqlMapper 实例
        /// </summary>
        private ISqlMapper _sqlMapper;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        SqlMapperHelper()
        {
            CreateSqlMapper();
        }
        #endregion

        #region 嵌套类
        class Nested
        {
            static Nested()
            {
            }
            internal static readonly SqlMapperHelper instance = new SqlMapperHelper();
        }
        #endregion

        #region 属性
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static SqlMapperHelper Instance
        {
            get
            {
                return Nested.instance;
            }
        }
        #endregion

        #region 公有方法
        /**/
        /// <summary>
        /// 刷新 ISqlMapper 对象
        /// 原因：当数据库连接出现变化，需要刷新该对象
        /// </summary>
        public void RefreshSqlMapper()
        {
            CreateSqlMapper();
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 创建 ISqlMapper 对象
        /// </summary>
        private void CreateSqlMapper()
        {
            //-----(1)
            //DomSqlMapBuilder builder = new DomSqlMapBuilder();
            //ISqlMapper _sqlMapper = builder.Configure("SqlMap.config");     
            //-----(2)
            //SqlMap是线程安全的
            //Mapper.Get()方法和Mapper.Instance()方法调用默认的 SqlMap.config 配置文件来创建SqlMapper
            //ISqlMapper sm = Mapper.Get();
            //与使用DomSqlMapBuilder类的区别是，Mapper.Get()不需要指定配置文件的名称，并且使用Mapper.Get()返回SqlMapper后如果映射的XML没有错误的话，会将该XML文件缓存到内存，
            //下次调用的时候就不需要在检查XML文件，直到SqlMap.config被改变,这样将大大的提高了程序的性能，而使用DomSqlMapBuilder建立的SqlMapper每次都要先分析映射的XML文件，性能将大大的降低


            DomSqlMapBuilder builder = new DomSqlMapBuilder();
            NameValueCollection prop = new NameValueCollection();


            string _ConntectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
            prop.Add("connectionString", _ConntectionString);
            builder.Properties = prop;
            try
            {
                _sqlMapper = builder.Configure();
                _sqlMapper.SessionStore = new HybridWebThreadSessionStore(_sqlMapper.Id);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        #endregion

        #region 对外属性
        /// <summary>
        /// ISqlMapper 实例
        /// </summary>
        public ISqlMapper SqlMapper
        {
            get
            {
                return _sqlMapper;
            }
        }
        #endregion

    }

}