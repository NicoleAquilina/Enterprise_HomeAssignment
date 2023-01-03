using Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class LogViaEmailRepository
    {
        private FileSharingContext context { get; set; }

        public LogViaEmailRepository(FileSharingContext _context)
        {
            context = _context;
        }


    }
}
