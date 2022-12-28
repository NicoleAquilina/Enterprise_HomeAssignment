using Application.ViewModels;
using Data.Repositories;
using Domain.Models;
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

        public void createTextFile(CreateTextFileViewModel tfvm)
        {
            TextFileModel tfm = new TextFileModel();

            tfm.FileName = tfvm.FileName;
            tfm.UploadedOn = DateTime.Now;
            tfm.Data = tfvm.Data;
            tfm.Author = "nicole";
            tfm.LastEditedBy = "nicole";
            tfm.LastUpdated = DateTime.Now;
           

            tr.Create(tfm);

            //giving permission
            tr.Share(tfm);
            
        }


    }
}
