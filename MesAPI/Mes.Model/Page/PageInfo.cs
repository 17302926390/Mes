using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mes.Model.Page
{
    public class PageInfo
    {
        // Properties

        public int CurrentPageIndex { get; set; }

        public string Order { get; set; }

        public int PageSize { get; set; }

        public string Sort { get; set; }

        public int Count { get; set; }

    }
}
