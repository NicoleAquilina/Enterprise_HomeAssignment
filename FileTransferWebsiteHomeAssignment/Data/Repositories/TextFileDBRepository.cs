using Data.Context;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Share (TextFileModel t)
        {
            //need to pass username (email) as a string to share

            AclModel a = new AclModel();
            a.FileName = t.FileName;
            a.Username = "Nicole";
            a.TextFileId = t.Id;
            a.TextFile = t;
            context.AclModels.Add(a);
            context.SaveChanges();
        }
        public TextFileModel GetFile(int id)
        {
            return context.TextFileModels.SingleOrDefault(x => x.Id == id);
        }

        public void Edit(TextFileModel updatedFile)
        {
            var originalFile = GetFile(updatedFile.Id);

            originalFile.Data = updatedFile.Data;
            originalFile.LastEditedBy = updatedFile.LastEditedBy;
            originalFile.LastUpdated = updatedFile.LastUpdated;

            context.SaveChanges();
        }
      


    }
}
