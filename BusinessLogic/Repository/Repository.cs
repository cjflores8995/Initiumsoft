﻿using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class Repository
    {
        protected ApplicationDbContext db = new ApplicationDbContext();
    }
}
