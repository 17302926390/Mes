using Mes.IServices;
using Mes.Model;
using Mes.Model.Page;
using Mes.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Mes.Api.Controllers
{
    /// <summary>
    /// 公告
    /// </summary>
    [RoutePrefix("api/AnnounCement")]
    public class AnnounCementController : BaseApiController
    {
        private readonly IAnnounCementServices _lloog;
        public AnnounCementController(IAnnounCementServices lloog)
        {
            _lloog = lloog;
        }

        /// <summary>
        /// 添加公告
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route("add")]
        [HttpPost]
        public async Task<IHttpActionResult> addAsync(ANNOUNCEMENTDTO dto)
        {
            try
            {
                var res = await _lloog.Add(dto);
                return Success(res, 0, "操作成功");
            }
            catch (System.Exception ex)
            {

                return Fail(ex.Message);
            }
           
        }

        /// <summary>
        /// 修改公告
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route("Update")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateAsync(ANNOUNCEMENTDTO dto)
        {
            try
            {
                var res = await _lloog.Update(dto);
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
        /// <param name="ANNOUNCEMENTNO"></param>
        /// <returns></returns>
        [Route("GetModel")]
        [HttpGet]
        public async Task<IHttpActionResult> GetModelAsync(string ANNOUNCEMENTNO)
        {
            try
            {
                var res = await _lloog.GetModel(ANNOUNCEMENTNO);
                return Success(res, 0, "操作成功");
            }
            catch (System.Exception ex)
            {

                return Fail(ex.Message);
            }
          
       
        }

        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="ANNOUNCEMENTNO"></param>
        /// <returns></returns>
        [Route("Delete")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAsync(string ANNOUNCEMENTNO)
        {
            try
            {
                var res = await _lloog.Delete(ANNOUNCEMENTNO);
                return Success(res, 0, "操作成功");
            }
            catch (System.Exception ex)
            {

                return Fail(ex.Message);
            }
          
        }

        /// <summary>
        /// 查询所有公告
        /// </summary>
        /// <returns></returns>
        [Route("GetList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetListAsync()
        {
            try
            {
                var res = await _lloog.GetList();
                return Success(res, 0, "操作成功");
            }
            catch (System.Exception ex)
            {

                return Fail(ex.Message);
            }
           
        }

        /// <summary>
        /// 公告分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetListPage")]
        public async Task<IHttpActionResult> GetListPageAsync(int pageIndex = 1, int pageSize = 10) {
            try
            {
                var (list, total) = await _lloog.GetListPage(pageIndex, pageSize);
                return Success(list, total, "操作成功");
            }
            catch (System.Exception ex)
            {

                return Fail(ex.Message);
            }

           
        }
    }
}