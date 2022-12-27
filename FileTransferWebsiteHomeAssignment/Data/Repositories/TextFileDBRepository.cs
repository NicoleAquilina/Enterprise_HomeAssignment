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

        public IQueryable<TextFileModel> GetFiles()
        {
            return context.TextFileModels;
        }

        public void AddItem(TextFileModel i)
        {
            context.TextFileModels.Add(i);
            context.SaveChanges();
        }

        

    }
}
