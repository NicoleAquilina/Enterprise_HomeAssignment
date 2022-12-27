using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ViewModels
{
    //selection on the required properties to be used by the presentation layer.
    public class CreateTextFileViewModel
    {
        public string Data { get; set; }
        public Guid FileName { get; set; }
        public DateTime UploadedOn { get; set; }
        public string Author { get; set; }
        //public string LastEditedBy { get; set; }
        //public DateTime LastUpdated { get; set; }
        public string FilePath { get; set; }
    }
}
