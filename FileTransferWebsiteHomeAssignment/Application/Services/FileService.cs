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
        
        public TextFileDBRepository tfr;
        public FileService (TextFileDBRepository _textFileDBRepository)
        {
            tfr = _textFileDBRepository;
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
           

            tfr.Create(tfm);

            //giving permission
            tfr.Share(tfm);
            
        }

        public IQueryable<TextFileViewModel>getFiles()
        {
            var list = from l in tfr.GetFilesEntries()
                       select new TextFileViewModel()
                       {
                           Id = l.Id,
                           Data = l.Data,
                           Author = l.Author,
                           UploadedOn = l.UploadedOn,
                           FileName = l.FileName,
                           LastEditedBy = l.LastEditedBy,
                           LastUpdated =l.LastUpdated
                       };
            return list;
        }

    }
}
