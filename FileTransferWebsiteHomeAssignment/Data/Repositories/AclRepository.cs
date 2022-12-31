using Data.Context;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class AclRepository : IAclRepository
    {
        private FileSharingContext context { get; set; }
        public  AclRepository(FileSharingContext _context)
        {
            context = _context;
        }

        public IQueryable<AclModel>GetPermissions()
        {
            return context.AclModels;

        }
    }
}
