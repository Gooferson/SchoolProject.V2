using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Shared.Domain
{
    public class Laptop : BaseDomainModel
    {
        public string? LaptopName { get; set; }
        public string? LaptopDescription { get; set; }
        public string? LaptopCategory { get; set; }
        public int? LaptopPrice { get; set; }
        public int CpuID { get; set; }
        public virtual Cpu? Cpu { get; set; }
        public int GpuID { get; set; }
        public virtual Gpu? Gpu { get; set; }
        public int OSID { get; set; }
        public virtual OS? OS { get; set; }
        public int RamID { get; set; }
        public virtual Ram? Ram { get; set; }
        public int ScreenID { get; set; }
        public virtual Screen? Screen { get; set; }
        public int WifiID { get; set; }
        public virtual Wifi? Wifi { get; set; }
    }
}
