using Dapper;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Mes.Services.dapper
{
    public static class DbConnectionFactory
    {
        private static IDbConnection dbConnection = null;

        /// <summary>
        /// 根据 Appsetting配置中的数据库类型和连接名，打开当前配置的数据库连接。
        /// </summary>
        /// <returns>
        /// </returns>
        public static IDbConnection OpenCurrentDbConnection(DbType dbType, string connection)
        {
            if (dbConnection != null)
            {
                dbConnection.Close();
                dbConnection = null;
            }
            switch (dbType)
            {
                case DbType.SqlServer:
                    if (dbConnection == null)
                    {
                        dbConnection = new SqlConnection(connection);
                    }
                    break;

                case DbType.MySQL:
                    if (dbConnection == null)
                    {
                        dbConnection = new MySqlConnection(connection);
                    }
                    break;

                case DbType.Oracle:
                    if (dbConnection == null)
                    {
                        dbConnection = new OracleConnection(connection);
                    }
                    break;

                case DbType.SQLite:
                    if (dbConnection == null)
                    {
                        dbConnection = new SqliteConnection(connection);
                    }
                    break;

                default:
                    break;
            }

            //判断连接状态
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }
            return dbConnection;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sql">查询sql</param>
        /// <param name="page">页码，可选参数，默认为 1</param>
        /// <param name="size">每页条数，可选参数，默认 15 条</param>
        /// <param name="param">sql查询参数</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>元组：（items: 查询对象集合，total: 总条数）</returns>
        public static (IEnumerable<T> items, int total) QueryPaging<T>(
            string sql, int page = 1, int size = 15, object param = null)
        {
            return QueryPagingAsync<T>(sql, page, size, param).Result;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sql">查询sql</param>
        /// <param name="page">页码，可选参数，默认为 1</param>
        /// <param name="size">每页条数，可选参数，默认 15 条</param>
        /// <param name="param">sql查询参数</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>元组：（items: 查询对象集合，total: 总条数）</returns>
        public static async Task<(IEnumerable<T> items, int total)> QueryPagingAsync<T>(
            string sql, int page = 1, int size = 15, object param = null)
        {
            if (string.IsNullOrWhiteSpace(sql) || page < 1 || size < 1)
                return (null, 0);

            var dynamicParameters = new DynamicParameters(param);
            dynamicParameters.Add("minRow", (page - 1) * size + 1);
            dynamicParameters.Add("maxRow", page * size);

            string countSql = $"SELECT COUNT(1) FROM ({sql})";

            // 用伪列 rownum 实现分页，注意以下几点：
            // 1. rownum 从 1 开始计数；
            // 2. rownum 的计算是在排序之前，所以要用嵌套子查询的方式让 sql 先执行排序（如果传入的sql中有）再计算 rownum；
            // 3. rownum 在经过查询的谓词阶段后才会分配，
            //    所以初始状态下不能直接对 rownum 使用 >/>=，因为此时还不存在比 1 大的 rownum，
            //    这里先对 rownum 做 <= 触发其从 1 开始分配，直到 maxRow 给定的值，
            //    然后再对分配好的行做 >= 取出最小值；
            StringBuilder rowsSql = new StringBuilder();
            rowsSql.Append("SELECT * ");
            rowsSql.Append("  FROM ( ");
            rowsSql.Append("       SELECT a.*, rownum rn ");
            rowsSql.Append($"        FROM ({sql}) a ");
            rowsSql.Append("        WHERE rownum <= :maxRow) ");
            rowsSql.Append(" WHERE rn >= :minRow ");

            var conn = dbConnection;
            try
            {
                int total = await conn.QueryFirstOrDefaultAsync<int>(countSql, dynamicParameters);
                var items = await conn.QueryAsync<T>(rowsSql.ToString(), dynamicParameters);
                return (items, total);
            }
            catch (Exception ex)
            {

                return (null, 0);
            }
            finally
            {
                conn?.Close();
                conn?.Dispose();
            }
        }
    }
}