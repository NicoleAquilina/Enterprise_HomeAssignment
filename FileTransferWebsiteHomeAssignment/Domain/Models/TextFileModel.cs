using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class TextFileModel
    { 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Data { get; set; }
        public Guid FileName { get; set; }
        public DateTime UploadedOn { get; set;}
        public string Author { get; set; }
        public DateTime LastUpdated { get; set; }

    }
}
