using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Shared.Domain
{
    public class Cpu:BaseDomainModel
    {
        public string? CpuBrand { get; set; }
        public string? CpuVersion { get; set; }
        public int? CpuCores { get; set; }
        public string? CpuSpeed { get; set;}
    }
}
