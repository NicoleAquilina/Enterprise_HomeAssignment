﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Interfaces
{
    public interface IAclRepository
    {
        IQueryable<AclModel> GetPermissions();
    }
}
