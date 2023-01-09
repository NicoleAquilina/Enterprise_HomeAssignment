using Data.Context;
using Domain.Interfaces;
using Domain.Models;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using MailKit.Net.Smtp;

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
            //implemented but not working correclty
            var mail = new MimeMessage();
            mail.From.Add(new MailboxAddress("Test Project", "fileTransfering@gmail.com"));
            mail.To.Add(new MailboxAddress("nicole", "nicole2611november@gmail.com"));
            mail.Subject = message + " by user " + user;
            mail.Body = new TextPart("plain")
            {
                Text = "Changes done are "+changes
            };
            using(var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                //client.Authenticate("fileTransfering@gmail.com","visualstudio");
                client.Send(mail);
                client.Disconnect(true);
            }
        }

        public void log(Exception ex, string user, string ipAddress)
        {
            throw new NotImplementedException();
        }
    }
}
