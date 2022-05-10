using Dapper;

using Mes.IServices;
using Mes.Model;
using Mes.Services.dapper;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mes.Services
{
    public class LogLoginServices : ILogLoginServices
    {
        private DbType dbType = DbType.SqlServer;
        private string connection = null;

        public LogLoginServices()
        {
            dbType = DbType.Oracle;
            connection = ConfigData.ConnectionString2;
        }

        /// <summary>
        /// 添加管理员
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> Add(ADMINISTRATORDTO dto)
        {
            using (var conn = DbConnectionFactory.OpenCurrentDbConnection(dbType, connection))
            {
                dto.ADMINISTRATORNO = Guid.NewGuid().ToString();
                dto.CREATETIME = DateTime.Now;
                string sql = $@"
             INSERT INTO WCCDB.ADMINISTRATOR
            (ADMINISTRATORNO, ADMINISTRATORNAME, ADMINISTRATORSEX, ADMINISTRATORAGE, ADMINISTRATORTEL, ADMINISTRATORPASSWORD, ADMINISTRATORMEAIL,CREATETIME)
            VALUES(:ADMINISTRATORNO, :ADMINISTRATORNAME, :ADMINISTRATORSEX, :ADMINISTRATORAGE, :ADMINISTRATORTEL, :ADMINISTRATORPASSWORD, :ADMINISTRATORMEAIL,:CREATETIME)
                       ";
                var items = await conn.ExecuteAsync(sql, dto);
                return items > 0;
            }
        }

        /// <summary>
        /// 修改管理员
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> Update(ADMINISTRATORDTO dto)
        {
            dto.UPDATETIME = DateTime.Now;
            using (var conn = DbConnectionFactory.OpenCurrentDbConnection(dbType, connection))
            {
                string sql = $@"UPDATE WCCDB.ADMINISTRATOR
                        SET
                        ADMINISTRATORNAME=:ADMINISTRATORNAME,
                        ADMINISTRATORSEX=:ADMINISTRATORSEX,
                        ADMINISTRATORAGE=:ADMINISTRATORAGE,
                        ADMINISTRATORTEL=:ADMINISTRATORTEL,
                        ADMINISTRATORPASSWORD=:ADMINISTRATORPASSWORD,
                        ADMINISTRATORMEAIL=:ADMINISTRATORMEAIL,
                         UPDATETIME=:UPDATETIME WHERE   ADMINISTRATORNO=:ADMINISTRATORNO
                 ";
                var items = await conn.ExecuteAsync(sql, dto);
                return items > 0;
            }
        }

        /// <summary>
        /// 获取实体类
        /// </summary>
        /// <param name="ADMINISTRATORNO"></param>
        /// <returns></returns>
        public async Task<ADMINISTRATORDTO> GetModel(string ADMINISTRATORNO)
        {
            using (var conn = DbConnectionFactory.OpenCurrentDbConnection(dbType, connection))
            {
                string sql = $@" SELECT ADMINISTRATORNO, ADMINISTRATORNAME, ADMINISTRATORSEX, ADMINISTRATORAGE, ADMINISTRATORTEL, ADMINISTRATORPASSWORD, ADMINISTRATORMEAIL, CREATETIME, UPDATETIME
                                 FROM WCCDB.ADMINISTRATOR where ADMINISTRATORNO=:ADMINISTRATORNO";
                var items = await conn.QueryFirstOrDefaultAsync<ADMINISTRATORDTO>(sql, new { ADMINISTRATORNO = ADMINISTRATORNO });
                return items;
            }
        }

        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="ADMINISTRATORNO"></param>
        /// <returns></returns>
        public async Task<bool> Delete(string ADMINISTRATORNO)
        {
            using (var conn = DbConnectionFactory.OpenCurrentDbConnection(dbType, connection))
            {
                string sql = $@"DELETE FROM WCCDB.ADMINISTRATOR
                         WHERE ADMINISTRATORNO=:ADMINISTRATORNO
                 ";
                var items = await conn.ExecuteAsync(sql, new { ADMINISTRATORNO = ADMINISTRATORNO });
                return items > 0;
            }
        }
        /// <summary>
        /// 查询所有管理员
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ADMINISTRATORDTO>> GetList()
        {
            using (var conn = DbConnectionFactory.OpenCurrentDbConnection(dbType, connection))
            {
                string sql = $@"SELECT ADMINISTRATORNO, ADMINISTRATORNAME, ADMINISTRATORSEX, ADMINISTRATORAGE, ADMINISTRATORTEL, ADMINISTRATORPASSWORD, ADMINISTRATORMEAIL, CREATETIME, UPDATETIME
                FROM WCCDB.ADMINISTRATOR

                 ";
                var items = await conn.QueryAsync<ADMINISTRATORDTO>(sql);
                return items;
            }
        }


        /// <summary>
        /// 管理员分页列表
        /// </summary>
        /// <param name="PAGEINDEX"></param>
        /// <param name="PAGESIZE"></param>
        /// <returns></returns>
        public async Task<(IEnumerable<ADMINISTRATORDTO>, int total)> GetListPage(long PAGEINDEX = 1, long PAGESIZE = 10)
        {
            DbConnectionFactory.OpenCurrentDbConnection(dbType, connection);
            var dynamicParams = new DynamicParameters();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM WCCDB.ADMINISTRATOR");
            return await DbConnectionFactory.QueryPagingAsync<ADMINISTRATORDTO>(sb.ToString(), Convert.ToInt32(PAGEINDEX), Convert.ToInt32(PAGESIZE), dynamicParams);

        }
    }
}