using Mes.IServices;
using Mes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Mes.Api.Controllers
{
    /// <summary>
    /// 学生
    /// </summary>
    [RoutePrefix("api/Student")]
    public class StudentController :  BaseApiController
    {

        private readonly IStudentServices _loog;

        public StudentController(IStudentServices loog)
        {
            _loog = loog;
        }

        /// <summary>
        /// 添加学生
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route("add")]
        [HttpPost]
        public async Task<IHttpActionResult> addAsync(STUDENTDTO dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.STUDENTNO)) {
                    return Fail("学生编码不能为空！");
                }
                var res = await _loog.Add(dto);
                return Success(res, 0, "操作成功");
            }
            catch (System.Exception ex)
            {
                return Fail(ex.Message);
            }

        }

        /// <summary>
        /// 修改学生
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route("Update")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateAsync(STUDENTDTO dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.STUDENTNO))
                {
                    return Fail("学生编码不能为空！");
                }
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
        /// <param name="STUDENTNO"></param>
        /// <returns></returns>
        [Route("GetModel")]
        [HttpGet]
        public async Task<IHttpActionResult> GetModelAsync(string STUDENTNO)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(STUDENTNO))
                {
                    return Fail("学生编码不能为空！");
                }
                var res = await _loog.GetModel(STUDENTNO);
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
        /// <param name="STUDENTNO"></param>
        /// <returns></returns>
        [Route("Delete")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAsync(string STUDENTNO)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(STUDENTNO))
                {
                    return Fail("学生编码不能为空！");
                }
                var ress = await _loog.GetModel(STUDENTNO);
                if (ress == null) {
                    return Fail("该学生已经被删除，请刷新页面！");
                }
                var res = await _loog.Delete(STUDENTNO);
                return Success(res, 0, "操作成功");
            }
            catch (System.Exception ex)
            {
                return Fail(ex.Message);
            }

        }

        /// <summary>
        /// 查询所有学生
        /// </summary>
        /// <returns></returns>
        [Route("GetList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetListAsync()
        {
            try
            {
                var res = await _loog.GetList();
                return Success(res, 0, "操作成功");
            }
            catch (System.Exception ex)
            {

                return Fail(ex.Message);
            }

        }

        /// <summary>
        /// 学生分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetListPage")]
        public async Task<IHttpActionResult> GetListPageAsync(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                var (list, total) = await _loog.GetListPage(pageIndex, pageSize);
                return Success(list, total, "操作成功");
            }
            catch (System.Exception ex)
            {
                return Fail(ex.Message);
            }

        }
    }
}
