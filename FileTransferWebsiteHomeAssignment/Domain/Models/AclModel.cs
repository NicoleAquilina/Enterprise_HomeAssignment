using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class AclModel
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }

       // [ForeignKey("TextFileModel")] still neded to check
        public Guid FileName { get; set; }
    }
}
