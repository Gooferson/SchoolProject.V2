using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Shared.Domain
{
    public class Gpu : BaseDomainModel
    {
        public string? GpuBrand { get; set; }
        public string? GpuVersion { get; set; }
        public int? GpuVram { get; set; }
        public int? GpuSpeed { get; set; }
    }
}
