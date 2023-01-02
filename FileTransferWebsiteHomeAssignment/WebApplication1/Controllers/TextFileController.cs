using Application.Services;
using Application.ViewModels;
using Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    //handling incoming  and outgoing requests
    public class TextFileController : Controller
    {
        private FileService service;
        private IWebHostEnvironment host;
        private FileSharingContext context;
        private AclServices aclService;
        public TextFileController(FileService _service, IWebHostEnvironment _host, FileSharingContext _context, AclServices _aclService)
        {
            service = _service;
            host = _host;
            context = _context;
            aclService = _aclService;
        }
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View(new CreateTextFileViewModel());
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(CreateTextFileViewModel data,IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    string username = User.Identity.Name; //gives you the email/username of the currently logged in user

                    if (file != null)
                    {
                        
                        Guid uniqueFilename = System.Guid.NewGuid();
                        string uniqueFilenameStr = uniqueFilename.ToString() + System.IO.Path.GetExtension(file.FileName);

                        string absolutePath = host.ContentRootPath; 

                        using (FileStream fsOut = new FileStream(absolutePath + "\\Data\\Files\\" + uniqueFilenameStr, FileMode.CreateNew))
                        {
                            file.CopyTo(fsOut);
                        }


                        data.FilePath = "\\Data\\Files\\" + uniqueFilenameStr;
                        data.FileName = uniqueFilename;
                        using (var readFile = new StreamReader(absolutePath + data.FilePath))
                        {
                            string fileContent = readFile.ReadToEnd();
                            data.Data = fileContent;
                        }
                    }

                    service.createTextFile(data,username.ToString());
                   // service.Create(data, username.ToString());
                    ViewBag.Message = "File successfully inserted in database";

                }


            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                //ViewBag.Error = "There Was a problem while saving the file.";
            }

            return View(data);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Share(int id)
        {
            string username = User.Identity.Name;
            AclViewModel model = aclService.getPermission(id,username.ToString());
            model.Users = service.GetAllUsers();
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Share(AclViewModel acl)
        {
            //avm.Username = User.Identity.Name;
            AclViewModel model = aclService.getPermissions().FirstOrDefault(f => f.Username == User.Identity.Name && f.TextFileId == acl.TextFileId);
            model.Users = service.GetAllUsers();

            if (service.Share(acl) == true)
            {
                ViewBag.Message = "File Shared!";
            }
            else
            {
                ViewBag.Error = "File Can't be shared";
            }
            return View(model);

        }
        [HttpGet]
        [Authorize]
        //this gets the data from the database
        public IActionResult Edit(int id)
        {
            var originalFile = service.getFile(id);

           TextFileViewModel model = new TextFileViewModel();
            model.Data = originalFile.Data;

            return View(model);
        }

        [Authorize]
        //this stores the newly edited data 
        public IActionResult Edit(int id, TextFileViewModel d)
        {
            try
            {
                string username = User.Identity.Name;
                var item = context.Users.Where(s => s.Email.Equals(username)).FirstOrDefault();
                var permission = aclService.getPermission(id, username.ToString());
                if (item!=null && permission!=null)
                {
                    service.Edit(id, d, username.ToString());

                    TempData["message"] = "File was updated!";
                }
                else
                {
                    TempData["error"] = "You dont have the permissions";

                }

            }
            catch (Exception ex)
            {
                TempData["error"] = "File wasm't updated successfully!";

            }
            return RedirectToAction("List");

        }

        public IActionResult List()
        {
            var list = service.getFiles();
            return View(list);
        }
    }
}
