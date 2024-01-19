using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Shared.Domain
{
    public class Screen : BaseDomainModel
    {
        public string? ScreenType { get; set; }
        public string? ScreenResolution { get; set; }
        public string? ScreenFinish { get; set; }
        public string? ScreenTouch { get; set; }
        public int? ScreenSie { get; set; }
        public int? ScreenHz { get; set; }
    }
}
