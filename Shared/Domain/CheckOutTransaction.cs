using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Shared.Domain
{
    public class CheckOutTransaction : BaseDomainModel
    {
        public virtual User? User { get; set; }
        public int? UserID { get; set; }
        public virtual Cart? Cart { get; set; }
        public int? CartID { get; set; }
    }
}
