using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mes.Model
{
    public class STUDENTDTO
    {
        public virtual string STUDENTNO { get; set; }
        public virtual string STUDENTNAME { get; set; }
        public virtual string STUDENTSEX { get; set; }
        public virtual string STUDENTNATION { get; set; }
        public virtual DateTime? ADMISSIONTIME { get; set; }
        public virtual string MAJOR { get; set; }
        public virtual string CULTIVATIONLEVEL { get; set; }
        public virtual string STUDENTTEL { get; set; }
        public virtual string STUDENTQQ { get; set; }
        public virtual string STUDENTPASSWORD { get; set; }
        public virtual string STUDENTEMAIL { get; set; }

        public virtual DateTime? CREATETIME { get; set; }

        public virtual DateTime? UPDATETIME { get; set; }
    }
}
