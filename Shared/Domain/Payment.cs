using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Shared.Domain
{
    public class Payment : BaseDomainModel
    {
        public int? Amount { get; set; }
        public int Method { get; set; }

    }
}
