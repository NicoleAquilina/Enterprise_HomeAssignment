using Application.Services;
using Application.ViewModels;
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

        public TextFileController(FileService _service, IWebHostEnvironment _host)
        {
            service = _service;
            host = _host;

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateTextFileViewModel());
        }

        [HttpPost]
        public IActionResult Create(CreateTextFileViewModel data,IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    //string username = User.Identity.Name; //gives you the email/username of the currently logged in user

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

                   
                    service.createTextFile(data);
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
    }
}
