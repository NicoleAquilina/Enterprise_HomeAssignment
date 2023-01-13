using Data.Context;
using Domain.Interfaces;
using Domain.Models;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Data.Repositories
{
    public class LogViaEmailRepository : ILogRepository
    {
        private FileSharingContext context { get; set; }
        
        public LogViaEmailRepository(FileSharingContext _context)
        {
            context = _context;
        }

        public void log(string message, string user, string changes, string ipAddress)
        {
            Log log = new Log()
            {
                Message = message,
                IpAddress = ipAddress,
                User = user,
                Timestamp = DateTime.Now
            };
            //do an outlook account
            SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("mvctesting123@outlook.com", "Mc@st123");

            MailMessage mail = new MailMessage();
            mail.To.Add("mvctesting123@outlook.com");
            mail.From = new MailAddress("mvctesting123@outlook.com");
            mail.Body = $"{log.Timestamp} - {log.User} - {log.IpAddress} - {log.Message}";
            mail.Subject = "Log File";
            smtpClient.Send(mail);
        }

        public void log(Exception ex, string user, string ipAddress)
        {
            Log log = new Log()
            {
                Message = $"Error Message {ex.Message} \n Inner Exception {ex.InnerException}",
                IpAddress = ipAddress,
                User = user,
                Timestamp = DateTime.Now
            };
            SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("mvctesting123@outlook.com", "Mc@st123");

            MailMessage mail = new MailMessage();
            mail.To.Add("mvctesting312@outlook.com");
            mail.From = new MailAddress("mvctesting123@oultook.com");
            mail.Body = $"{log.Timestamp} - {log.User} - {log.IpAddress} - {log.Message}";
            mail.Subject = "Log File";
            smtpClient.Send(mail);
        }
    }
}
