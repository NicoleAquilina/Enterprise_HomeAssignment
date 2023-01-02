using Data.Context;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Data.Repositories
{
    public class TextFileDBRepository
    {
        private FileSharingContext context { get; set; }

        public TextFileDBRepository(FileSharingContext _context)
        {
            context = _context;
        }

        public IQueryable<TextFileModel> GetFilesEntries()
        {
            return context.TextFileModels;
        }

        public void Create(TextFileModel i)
        {
            context.TextFileModels.Add(i);
            context.SaveChanges();
        }

        public IQueryable<AclModel> GetPermissions()
        { return context.AclModels; }

        public void Share (TextFileModel t, string username)
        {

            AclModel a = new AclModel();
            a.FileName = t.FileName;
            a.Username = username;
            a.TextFileId = t.Id;
            a.TextFile = t;
            context.AclModels.Add(a);
            context.SaveChanges();
        }
        public Boolean checkHashCode(TextFileModel f)
        {
            string updatedHashedData = "";
            using (MD5 hash = MD5.Create())
            {
                updatedHashedData = Convert.ToBase64String(hash.ComputeHash(Encoding.UTF8.GetBytes(f.Data)));
            }
            TextFileModel currentFile = GetFile(f.Id);
            if(updatedHashedData == currentFile.DataHash)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public TextFileModel GetFile(int id)
        {
            return context.TextFileModels.SingleOrDefault(x => x.Id == id);
        }

        public void Edit(int id, TextFileModel updatedFile)
        {
            
            var originalFile = GetFile(id);

            originalFile.Data = updatedFile.Data;
            originalFile.LastEditedBy = updatedFile.LastEditedBy;
            originalFile.LastUpdated = updatedFile.LastUpdated;

            string updatedHashCode = "";
            using (MD5 hash = MD5.Create())
            {
                updatedHashCode = Convert.ToBase64String(hash.ComputeHash(Encoding.UTF8.GetBytes(updatedFile.Data)));
            }
            originalFile.DataHash = updatedHashCode;
            context.SaveChanges();
        }
        public IQueryable<CustomUser> GetUsers()
        {
            return context.Users;
        }

    }
}
