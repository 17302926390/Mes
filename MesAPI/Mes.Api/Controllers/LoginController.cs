using Mes.IServices;
using Mes.Model;
using Mes.Model.Page;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Mes.Api.Controllers
{
    /// <summary>
    /// 管理员
    /// </summary>
    [RoutePrefix("api/Login")]
    public class LoginController : BaseApiController
    {
        private readonly ILogLoginServices _loog;

        public LoginController(ILogLoginServices loog)
        {
            _loog = loog;
        }

        /// <summary>
        /// 添加管理员
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route("add")]
        [HttpPost]
        public async Task<IHttpActionResult> addAsync(ADMINISTRATORDTO dto)
        {
            try
            {
                var res = await _loog.Add(dto);
                return Success(res, 0, "操作成功");
            }
            catch (System.Exception ex)
            {
                return Fail(ex.Message);
            }
         
        }

        /// <summary>
        /// 修改管理员
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route("Update")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateAsync(ADMINISTRATORDTO dto)
        {
            try
            {
                var res = await _loog.Update(dto);
                return Success(res, 0, "操作成功");
            }
            catch (System.Exception ex)
            {

                return Fail(ex.Message);
            }
            
        }

        /// <summary>
        /// 获取实体类
        /// </summary>
        /// <param name="ADMINISTRATORNO"></param>
        /// <returns></returns>
        [Route("GetModel")]
        [HttpGet]
        public async Task<IHttpActionResult> GetModelAsync(string ADMINISTRATORNO)
        {
            try
            {
                var res = await _loog.GetModel(ADMINISTRATORNO);
                return Success(res, 0, "操作成功");
            }
            catch (System.Exception ex)
            {

                return Fail(ex.Message);
            }

        }

        /// <summary>
        /// 删除实体类
        /// </summary>
        /// <param name="ADMINISTRATORNO"></param>
        /// <returns></returns>
        [Route("Delete")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAsync(string ADMINISTRATORNO)
        {
            try
            {
                var res = await _loog.Delete(ADMINISTRATORNO);
                return Success(res, 0, "操作成功");
            }
            catch (System.Exception ex)
            {
                return Fail(ex.Message);
            }
          
        }

        /// <summary>
        /// 查询所有管理员
        /// </summary>
        /// <returns></returns>
        [Route("GetList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetListAsync()
        {
            try
            {
                var res = await _loog.GetList();
                return Success(res, 0,"操作成功");
            }
            catch (System.Exception ex)
            {

                return Fail(ex.Message);
            }
        
        }

        /// <summary>
        /// 管理员分页列表
        /// </summary>
        /// <param name="current"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetListPage")]
        public async Task<IHttpActionResult> GetListPageAsync(int current = 1, int size = 10)
        {
            try
            {
                var (list, total) = await _loog.GetListPage(current, size);
                return Success(list, total,"操作成功");
            }
            catch (System.Exception ex)
            {
                return Fail(ex.Message);
            }
           
        }
    }
}