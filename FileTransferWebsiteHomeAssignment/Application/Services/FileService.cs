using Application.ViewModels;
using Data.Repositories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class FileService
    {

        private TextFileDBRepository tfr;
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
            //https://social.msdn.microsoft.com/Forums/en-US/bd1085fe-f042-4dd4-a9f2-cd0704b22e36/how-to-convert-text-into-md5?forum=aspgettingstarted
            using (MD5 hash = MD5.Create())
            {
                tfm.DataHash = Convert.ToBase64String(hash.ComputeHash(Encoding.UTF8.GetBytes(tfm.Data)));
            }

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

        //public void           
        public Boolean Edit(int id, TextFileViewModel updatedFile, string username)
        {
            TextFileModel currentFile = tfr.GetFile(id);
            if(tfr.checkHashCode(currentFile) == true)
            {
                tfr.Edit
                (id, new Domain.Models.TextFileModel()
                {
                    Data = updatedFile.Data,
                    LastUpdated = DateTime.Now,
                    LastEditedBy = username
                }
                );

                return true;
            }
            else
            {
                return false;
            }
        
            
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
