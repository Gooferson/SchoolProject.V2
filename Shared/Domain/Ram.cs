using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Shared.Domain
{
    public class Ram : BaseDomainModel
    {
        public string? RamBrand { get; set; }
        public int? RamAmount { get; set; }
        public int? RamSpeed { get; set; }
    }
}
