using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.ViewModels
{
    public class AclViewModel
    {
        public int Id { get; set; }
        public Guid FileName { get; set; }
        public string Username { get; set; }
        public int TextFileId { get; set; }
        public IQueryable<CustomUser> Users { get; set; }
    }
}
