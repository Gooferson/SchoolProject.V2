using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Shared.Domain
{
    public class User : BaseDomainModel
    {
        public string? Password { get; set; }
        public string? Username { get; set; }
        public virtual Cart? Cart { get; set; }
        public int? CartID { get; set; }

    }
}
