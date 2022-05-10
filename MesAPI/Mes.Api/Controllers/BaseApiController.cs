using Mes.Model.Page;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mes.Api.Controllers
{

    public class BaseApiController : ApiController
    {
        /// <summary>
        /// 构建操作结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private ResponseResult<T> BuildResult<T>(bool isSuccess, T data = default, int total = 0, string msg = null)
        {
            var result = new ResponseResult<T>();

            if (isSuccess)
                result.Success(data, total, msg);
            else
                result.Fail(string.IsNullOrWhiteSpace(msg) ? "操作失败" : msg);

            return result;
        }

        /// <summary>
        /// 操作成功
        /// </summary>
        /// <param name="data"></param>
        /// <param name="total"></param>
        /// <param name="msg"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected IHttpActionResult Success<T>(T data, int total = 0, string msg = null)
        {
            return Ok(BuildResult(true, data, total, msg));
        }

        /// <summary>
        /// 操作失败
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected IHttpActionResult Fail(string msg = null)
        {
            return Ok(BuildResult<object>(false, msg: msg));
        }


    }
}
