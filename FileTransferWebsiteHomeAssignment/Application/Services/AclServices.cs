using Application.ViewModels;
using Data.Repositories;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Services
{
    public class AclServices
    {
        private IAclRepository ar;

        public AclServices(IAclRepository _aclRepository)
        {
            ar = _aclRepository;
        }
       public IQueryable<AclViewModel>getPermissions()
       {
           var list = from p in ar.GetPermissions()
                      select new AclViewModel()
                      {
                          Username = p.Username,
                          TextFileId = p.TextFileId
                      };
           return list;
       }
       public AclViewModel getPermission(int fileId , string username)
       {
           return getPermissions().SingleOrDefault(x => x.TextFileId == fileId && x.Username == username);
       }

       
    }
}
