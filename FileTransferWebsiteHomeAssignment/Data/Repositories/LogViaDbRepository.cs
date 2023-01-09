using Data.Context;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class LogViaDbRepository: ILogRepository
    {
        private FileSharingContext context { get; set; }

        public LogViaDbRepository(FileSharingContext _context)
        {
            context = _context;
        }

        public void log(string message, string user, string changes, string ipAddress)
        {
            try
            {
                Log l = new Log();
                l.Message = message;
                l.User = user;
                l.Changes = changes;
                l.Timestamp = DateTime.Now;
                l.IpAddress = ipAddress;
                context.Logs.Add(l);
                context.SaveChanges();

            }catch(Exception ex)
            {
                log(ex,user,ipAddress);
            }
        }

        public void log(Exception ex,string user,string ipAddress)
        {
            Log l = new Log();
            l.Message = ex.Message;
            l.User = user;
            l.IpAddress = ipAddress;
            l.Timestamp = DateTime.Now;
            l.Changes = "";
            context.Logs.Add(l);
            context.SaveChanges();
        }
    }
}
