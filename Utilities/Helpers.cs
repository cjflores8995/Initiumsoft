﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Helpers: IHelpers
    {
        public string GenerateIdentifier()
        {
            return DateTime.Now.Ticks.ToString();
        }
    }
}
