using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mes.Model
{
    public class ANNOUNCEMENTDTO
    {

        public virtual string ANNOUNCEMENTNO { get; set; }
        public virtual string ADMINISTRATORNO { get; set; }
        public virtual string ANNOUNCEMENTTITLE { get; set; }
        public virtual string ANNOUNCEMENTCONTENT { get; set; }
        public virtual DateTime? CREATETIME { get; set; }
        public virtual DateTime? UPDATETIME { get; set; }
    }

}
