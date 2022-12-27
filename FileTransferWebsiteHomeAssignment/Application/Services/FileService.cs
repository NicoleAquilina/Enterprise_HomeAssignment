using Application.ViewModels;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Services
{
    public class FileService
    {
        private TextFileDBRepository tr;

        public FileService (TextFileDBRepository _textRepository)
        {
            tr = _textRepository;
        }

        public void createTextFile(CreateTextFileViewModel txtfile)
        {
            if (tr.GetFiles().Any(i => i.FileName == i.FileName))
                throw new Exception("File with the same name already exists");
            else
            {
                tr.AddItem(new Domain.Models.TextFileModel()
                {
                    Data = txtfile.Data,
                    FileName = txtfile.FileName,
                    UploadedOn = txtfile.UploadedOn,
                    Author = txtfile.Author,
                    FilePath = txtfile.FilePath
                });
            }
        }


    }
}
