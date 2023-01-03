using Data.Context;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class LogViaDbRepository
    {
        private FileSharingContext context { get; set; }

        public LogViaDbRepository(FileSharingContext _context)
        {
            context = _context;
        }

        public void log (string message,string username, string changes, string iphost)
        {
            try
            {
                Log log = new Log();
                log.Message = message;
                log.User = username;
                log.Changes = changes;
                log.Timestamp = DateTime.Now;
                log.IpAddress = iphost;
                context.Logs.Add(log);
                context.SaveChanges();
            }catch(Exception ex)
            {
                log(ex);
            }
        }

        //dont know exactly what to do
        public string log(Exception ex)
        {
            return "helo";
        }

    }
}
