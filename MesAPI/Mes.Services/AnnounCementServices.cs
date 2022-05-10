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
    public class AnnounCementServices:IAnnounCementServices
    {

        private DbType dbType = DbType.SqlServer;
        private string connection = null;

        public AnnounCementServices()
        {
            dbType = DbType.Oracle;
            connection = ConfigData.ConnectionString2;
        }


        /// <summary>
        /// 添加公告
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> Add(ANNOUNCEMENTDTO dto)
        {
            dto.ANNOUNCEMENTNO=Guid.NewGuid().ToString();
            dto.CREATETIME=DateTime.Now;
            using (var conn = DbConnectionFactory.OpenCurrentDbConnection(dbType, connection))
            {
                string sql = $@"INSERT INTO WCCDB.ANNOUNCEMENT
            (ANNOUNCEMENTNO, ADMINISTRATORNO, ANNOUNCEMENTTITLE, ANNOUNCEMENTCONTENT, CREATETIME)
            VALUES(:ANNOUNCEMENTNO, :ADMINISTRATORNO, :ANNOUNCEMENTTITLE, :ANNOUNCEMENTCONTENT, :CREATETIME)
            ";
                var items = await conn.ExecuteAsync(sql, dto);
                return items > 0;

            }

        }



        /// <summary>
        /// 修改公告
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> Update(ANNOUNCEMENTDTO dto)
        {
            dto.UPDATETIME = DateTime.Now;
            using (var conn = DbConnectionFactory.OpenCurrentDbConnection(dbType, connection))
            {
                string sql = $@"UPDATE WCCDB.ANNOUNCEMENT
                SET  ANNOUNCEMENTTITLE=:ANNOUNCEMENTTITLE, ANNOUNCEMENTCONTENT=:ANNOUNCEMENTCONTENT, UPDATETIME=:UPDATETIME
                WHERE ANNOUNCEMENTNO=:ANNOUNCEMENTNO
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
        public async Task<ANNOUNCEMENTDTO> GetModel(string ANNOUNCEMENTNO)
        {
            using (var conn = DbConnectionFactory.OpenCurrentDbConnection(dbType, connection))
            {
                string sql = $@" SELECT ANNOUNCEMENTNO, ADMINISTRATORNO, ANNOUNCEMENTTITLE, ANNOUNCEMENTCONTENT, CREATETIME, UPDATETIME
                FROM WCCDB.ANNOUNCEMENT where ANNOUNCEMENTNO=:ANNOUNCEMENTNO";
                var items = await conn.QueryFirstOrDefaultAsync<ANNOUNCEMENTDTO>(sql, new { ANNOUNCEMENTNO = ANNOUNCEMENTNO });
                return items;
            }
        }

        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="ADMINISTRATORNO"></param>
        /// <returns></returns>
        public async Task<bool> Delete(string ANNOUNCEMENTNO)
        {
            using (var conn = DbConnectionFactory.OpenCurrentDbConnection(dbType, connection))
            {
                string sql = $@"DELETE FROMWCCDB.ANNOUNCEMENT
                         WHERE ANNOUNCEMENTNO=:ANNOUNCEMENTNO
                 ";
                var items = await conn.ExecuteAsync(sql, new { ANNOUNCEMENTNO = ANNOUNCEMENTNO });
                return items > 0;
            }
        }
        /// <summary>
        /// 查询所有公告
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ANNOUNCEMENTDTO>> GetList()
        {
            using (var conn = DbConnectionFactory.OpenCurrentDbConnection(dbType, connection))
            {
                string sql = $@"SELECT ANNOUNCEMENTNO, ADMINISTRATORNO, ANNOUNCEMENTTITLE, ANNOUNCEMENTCONTENT, CREATETIME, UPDATETIME
              FROM WCCDB.ANNOUNCEMENT

                 ";
                var items = await conn.QueryAsync<ANNOUNCEMENTDTO>(sql);
                return items;
            }
        }


        /// <summary>
        /// 公告分页列表
        /// </summary>
        /// <param name="PAGEINDEX"></param>
        /// <param name="PAGESIZE"></param>
        /// <returns></returns>
        public async Task<(IEnumerable<ANNOUNCEMENTDTO>, int total)> GetListPage(long PAGEINDEX = 1, long PAGESIZE = 10)
        {
            DbConnectionFactory.OpenCurrentDbConnection(dbType, connection);
            var dynamicParams = new DynamicParameters();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM WCCDB.ANNOUNCEMENT");
            return await DbConnectionFactory.QueryPagingAsync<ANNOUNCEMENTDTO>(sb.ToString(), Convert.ToInt32(PAGEINDEX), Convert.ToInt32(PAGESIZE), dynamicParams);

        }
    }
}
