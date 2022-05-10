using Mes.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mes.IServices
{
    public interface ILogLoginServices
    {
        Task<ADMINISTRATORDTO> GetModel(string ADMINISTRATORNO);

        Task<bool> Add(ADMINISTRATORDTO dto);

        Task<bool> Update(ADMINISTRATORDTO dto);

        Task<bool> Delete(string ADMINISTRATORNO);
        Task<IEnumerable<ADMINISTRATORDTO>> GetList();

        Task<(IEnumerable<ADMINISTRATORDTO>, int total)> GetListPage(long PAGEINDEX = 1, long PAGESIZE = 10);
    }
}