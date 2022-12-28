using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ViewModels
{
    //selection on the required properties to be used by the presentation layer.
    public class CreateTextFileViewModel
    {
        public string Data { get; set; }
        public string FilePath { get; set; }
        public Guid FileName { get; set; }
    }
}
