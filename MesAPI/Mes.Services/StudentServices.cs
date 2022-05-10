using Dapper;

using Mes.IServices;
using Mes.Model;
using Mes.Services.dapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mes.Services
{
   public class StudentServices: IStudentServices
    {
        private DbType dbType = DbType.SqlServer;
        private string connection = null;

        public StudentServices()
        {
            dbType = DbType.Oracle;
            connection = ConfigData.ConnectionString2;
        }

        /// <summary>
        /// 添加学生
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> Add(STUDENTDTO dto)
        {
            using (var conn = DbConnectionFactory.OpenCurrentDbConnection(dbType, connection))
            {
                dto.STUDENTNO = Guid.NewGuid().ToString();
                dto.CREATETIME = DateTime.Now;
                string sql = $@"
                       INSERT INTO WCCDB.STUDENT
                        (STUDENTNO, STUDENTNAME, STUDENTSEX, STUDENTNATION, ADMISSIONTIME, MAJOR, CULTIVATIONLEVEL, STUDENTTEL, STUDENTQQ, STUDENTPASSWORD, STUDENTEMAIL, CREATETIME)
                        VALUES(:STUDENTNO, :STUDENTNAME, :STUDENTSEX, :STUDENTNATION, :ADMISSIONTIME, :MAJOR, :CULTIVATIONLEVEL, :STUDENTTEL, :STUDENTQQ, :STUDENTPASSWORD, :STUDENTEMAIL, :CREATETIME)
                                               ";
                var items = await conn.ExecuteAsync(sql, dto);
                return items > 0;
            }
        }

        /// <summary>
        /// 修改学生
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> Update(STUDENTDTO dto)
        {
            dto.UPDATETIME = DateTime.Now;
            using (var conn = DbConnectionFactory.OpenCurrentDbConnection(dbType, connection))
            {
                string sql = $@"UPDATE WCCDB.STUDENT
                                    SET STUDENTNAME=:STUDENTNAME, 
                                    STUDENTSEX=:STUDENTSEX, 
                                    STUDENTNATION=:STUDENTNATION,
                                    ADMISSIONTIME=:ADMISSIONTIME,
                                    MAJOR=:MAJOR, 
                                    CULTIVATIONLEVEL=:CULTIVATIONLEVEL, 
                                    STUDENTTEL=:STUDENTTEL,
                                    STUDENTQQ=:STUDENTQQ, 
                                    STUDENTPASSWORD=:STUDENTPASSWORD, 
                                    STUDENTEMAIL=:STUDENTEMAIL, 
                                    UPDATETIME=:UPDATETIME
                                    WHERE STUDENTNO=:STUDENTNO
                 ";
                var items = await conn.ExecuteAsync(sql, dto);
                return items > 0;
            }
        }

        /// <summary>
        /// 获取实体类
        /// </summary>
        /// <param name="STUDENTNO"></param>
        /// <returns></returns>
        public async Task<STUDENTDTO> GetModel(string STUDENTNO)
        {
            using (var conn = DbConnectionFactory.OpenCurrentDbConnection(dbType, connection))
            {
                string sql = $@" SELECT STUDENTNO, STUDENTNAME, STUDENTSEX, STUDENTNATION, ADMISSIONTIME, MAJOR, CULTIVATIONLEVEL, STUDENTTEL, STUDENTQQ, STUDENTPASSWORD, STUDENTEMAIL, CREATETIME, UPDATETIME
               FROM WCCDB.STUDENT where STUDENTNO=:STUDENTNO";
                var items = await conn.QueryFirstOrDefaultAsync<STUDENTDTO>(sql, new { STUDENTNO = STUDENTNO });
                return items;
            }
        }

        /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="STUDENTNO"></param>
        /// <returns></returns>
        public async Task<bool> Delete(string STUDENTNO)
        {
            using (var conn = DbConnectionFactory.OpenCurrentDbConnection(dbType, connection))
            {
                string sql = $@"DELETE FROM WCCDB.STUDENT
                         WHERE STUDENTNO=:STUDENTNO
                 ";
                var items = await conn.ExecuteAsync(sql, new { STUDENTNO = STUDENTNO });
                return items > 0;
            }
        }
        /// <summary>
        /// 查询所有学生
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<STUDENTDTO>> GetList()
        {
            using (var conn = DbConnectionFactory.OpenCurrentDbConnection(dbType, connection))
            {
                string sql = $@"SELECT STUDENTNO, STUDENTNAME, STUDENTSEX, STUDENTNATION, ADMISSIONTIME, MAJOR, CULTIVATIONLEVEL, STUDENTTEL, STUDENTQQ, STUDENTPASSWORD, STUDENTEMAIL, CREATETIME, UPDATETIME
               FROM WCCDB.STUDENT

                 ";
                var items = await conn.QueryAsync<STUDENTDTO>(sql);
                return items;
            }
        }


        /// <summary>
        /// 学生分页列表
        /// </summary>
        /// <param name="PAGEINDEX"></param>
        /// <param name="PAGESIZE"></param>
        /// <returns></returns>
        public async Task<(IEnumerable<STUDENTDTO>, int total)> GetListPage(long PAGEINDEX = 1, long PAGESIZE = 10)
        {
            DbConnectionFactory.OpenCurrentDbConnection(dbType, connection);
            var dynamicParams = new DynamicParameters();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM WCCDB.STUDENT");
            return await DbConnectionFactory.QueryPagingAsync<STUDENTDTO>(sb.ToString(), Convert.ToInt32(PAGEINDEX), Convert.ToInt32(PAGESIZE), dynamicParams);

        }
    }
}
