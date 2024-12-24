﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomFileCach
{
    public class FileCacheOptions
    {
        public TimeProvider TimeProvider { get; set; } = TimeProvider.System;
        public TimeSpan? DefaultExpiry { get; set; }
    }
}
