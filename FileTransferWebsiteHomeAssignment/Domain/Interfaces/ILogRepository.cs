using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface ILogRepository
    {
        public void log(string message , string user, string changes , string ipAddress);

        public void log(Exception ex,string user,string ipAddress);
    }
}
