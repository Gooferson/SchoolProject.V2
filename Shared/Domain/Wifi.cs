﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Shared.Domain
{
    public class Wifi : BaseDomainModel
    {
        public string? WifiBrand { get; set; }
        public string? WifiName { get; set; }
        public string? WifiType { get; set; }

    }
}
