using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Shared.Domain
{
    public class Cart : BaseDomainModel
    {
        public int? CartQuantity { get; set; }
        public int? LaptopID { get; set; }
        public virtual Laptop? Laptop { get; set; }

    }
}
