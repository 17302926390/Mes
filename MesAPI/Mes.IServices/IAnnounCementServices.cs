using Mes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mes.IServices
{
    public interface IAnnounCementServices
    {
        Task<ANNOUNCEMENTDTO> GetModel(string ANNOUNCEMENTNO);

        Task<bool> Add(ANNOUNCEMENTDTO dto);

        Task<bool> Update(ANNOUNCEMENTDTO dto);

        Task<bool> Delete(string ANNOUNCEMENTNO);
        Task<IEnumerable<ANNOUNCEMENTDTO>> GetList();

        Task<(IEnumerable<ANNOUNCEMENTDTO>, int total)> GetListPage(long PAGEINDEX = 1, long PAGESIZE = 10);
    }
}
