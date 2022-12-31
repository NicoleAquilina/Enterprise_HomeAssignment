﻿using Application.ViewModels;
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
        public FileService(TextFileDBRepository _textFileDBRepository)
        {
            tfr = _textFileDBRepository;
        }

        public void createTextFile(CreateTextFileViewModel tfvm, string username)
        {

            //need to pass username (email) as a string to share
            TextFileModel tfm = new TextFileModel();

            tfm.FileName = tfvm.FileName;
            tfm.UploadedOn = DateTime.Now;
            tfm.Data = tfvm.Data;
            tfm.Author = username;
            tfm.LastEditedBy = username;
            tfm.LastUpdated = DateTime.Now;


            tfr.Create(tfm);

            //giving permission
            tfr.Share(tfm, username);

        }

        public IQueryable<TextFileViewModel> getFiles()
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
                           LastUpdated = l.LastUpdated
                       };
            return list;
        }
       
        public TextFileViewModel getFile(int id)
        {
            return getFiles().SingleOrDefault(x => x.Id == id);
        }

        public void Edit(int id, TextFileViewModel updatedFile, string username)
        {
            tfr.Edit
                (id, new Domain.Models.TextFileModel()
                {
                    Data = updatedFile.Data,
                    LastUpdated = DateTime.Now,
                    LastEditedBy = username
                }
           );
        }

        public IQueryable<CustomUser> GetAllUsers()
        {
            return tfr.GetUsers();
        }

        public Boolean Share(AclViewModel acl)
        {

            if (tfr.GetPermissions().Where(a => a.Username == acl.Username && a.FileName == acl.FileName).Count() == 0)
            {
                Guid fileId = acl.FileName;
                TextFileModel t = tfr.GetFile(acl.Id);
                tfr.Share(t, acl.Username);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
