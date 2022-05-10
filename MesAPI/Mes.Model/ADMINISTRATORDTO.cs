using System;

namespace Mes.Model
{
    public class ADMINISTRATORDTO
    {
        public virtual string ADMINISTRATORNO { get; set; }
        public virtual string ADMINISTRATORNAME { get; set; }
        public virtual string ADMINISTRATORSEX { get; set; }
        public virtual string ADMINISTRATORAGE { get; set; }
        public virtual string ADMINISTRATORTEL { get; set; }
        public virtual string ADMINISTRATORPASSWORD { get; set; }
        public virtual string ADMINISTRATORMEAIL { get; set; }

        public virtual DateTime? CREATETIME { get; set; }

        public virtual DateTime? UPDATETIME { get; set; }
    }
}