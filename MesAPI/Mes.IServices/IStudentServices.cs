using Mes.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mes.IServices
{
   public   interface IStudentServices
    {
        Task<STUDENTDTO> GetModel(string STUDENTNO);

        Task<bool> Add(STUDENTDTO dto);

        Task<bool> Update(STUDENTDTO dto);

        Task<bool> Delete(string STUDENTNO);
        Task<IEnumerable<STUDENTDTO>> GetList();

        Task<(IEnumerable<STUDENTDTO>, int total)> GetListPage(long PAGEINDEX = 1, long PAGESIZE = 10);
    }
}
