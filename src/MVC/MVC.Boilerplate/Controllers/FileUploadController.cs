using Microsoft.AspNetCore.Mvc;
using MVC.Boilerplate.Models.FileUpload;
using System.IO;
using System.Data;
using System.Windows;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace MVC.Boilerplate.Controllers
{
    public class FileUploadController : Controller
    {

        private readonly ILogger<FileUploadController> _logger;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly INotyfService _notyf;
        private readonly IConfiguration _configuration;

        public FileUploadController(ILogger<FileUploadController> logger, INotyfService notyf, IWebHostEnvironment webHost, IConfiguration configuration)
        {
            _logger = logger;
            webHostEnvironment = webHost;
            _notyf = notyf;
            _configuration = configuration;
        }

        public IActionResult Index(FileUploadModel fileUploadModel)
        {
            FileUploadErrorMessage fileUploadErrorMessage = new FileUploadErrorMessage();
            UploadFile(fileUploadModel);

            if (fileUploadModel.File != null)
                ViewData["File"] = fileUploadModel.File.FileName;

            var file = _configuration.GetSection("FileUploadSettings").GetSection("Disclaimer").GetSection("AllowedFiles").Value;
            ViewBag.TypeDisclaimer = _configuration["FileUploadSettings:Disclaimer:AllowedFiles"];
            ViewBag.SizeDisclaimer = _configuration.GetSection("FileUploadSettings").GetSection("Disclaimer").GetSection("MaxSize").Value;




            return View();
        }


        private string UploadFile(FileUploadModel fileUploadModel)
        {
            string uniqueFileName = null;
            if (fileUploadModel.File != null)
            {
                FileUploadErrorMessage fileUploadErrorMessage = new FileUploadErrorMessage();
                fileUploadErrorMessage.filesize = 32;
                var filename = _configuration.GetSection("FileUploadSettings").GetSection("FilePath").Value;
                string us = FileValidation(fileUploadModel);
                if (us != null)
                {
                    ViewBag.ResultErrorMessage = fileUploadErrorMessage.ErrorMessage;
                    _notyf.Error(fileUploadErrorMessage.ErrorMessage);
                }
                else
                {
                    string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, $"{filename}");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + fileUploadModel.File.FileName;
                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        fileUploadModel.FileUrl = fileStream.Name;
                        fileUploadModel.File.CopyTo(fileStream);
                    }
                    ViewData["FileUploadPath"] = filePath;
                    ViewData["FileUploadName"] = uniqueFileName;

                    _notyf.Success("File Is Successfully Uploaded");
                }
            }
            return uniqueFileName;
        }
        public string FileValidation(FileUploadModel fileUploadModel)
        {
            FileUploadErrorMessage fileUploadErrorMessage = new FileUploadErrorMessage();
            fileUploadErrorMessage.filesize = Convert.ToInt32(_configuration.GetSection("FileUploadSettings").GetSection("MaxFileSizeMb").Value);
            try
            {
                var supportedTypes = _configuration.GetSection("FileUploadSettings").GetSection("AllowedFileExtension").Value;
                var fileTypes= supportedTypes.Split(',');
                var fileExt = System.IO.Path.GetExtension(fileUploadModel.File.FileName).Substring(1);
                if (!fileTypes.Contains(fileExt))
                {
                    fileUploadErrorMessage.ErrorMessage = _configuration.GetSection("FileUploadSettings").GetSection("FileNotAllowedErrorMessage").Value;
                }
                else if (fileUploadModel.File.Length > (fileUploadErrorMessage.filesize * 1024 * 1024))
                {
                    fileUploadErrorMessage.ErrorMessage = _configuration.GetSection("FileUploadSettings").GetSection("FileSizeExceedErrorMessage").Value;
                }
                else
                {
                    fileUploadErrorMessage.ErrorMessage = null;
                }
                return fileUploadErrorMessage.ErrorMessage;
            }
            catch (Exception ex)
            {
                fileUploadErrorMessage.ErrorMessage = "Upload Container Should Not Be Empty or Contact Admin";
                return fileUploadErrorMessage.ErrorMessage;
            }
        }


        public IActionResult Delete(string FileName)
        {
            if (FileName != null)
            {
                string ExitingFile = Path.Combine(webHostEnvironment.WebRootPath, "FileFolder/") + FileName;

                System.IO.File.Delete(ExitingFile);
            }
            return RedirectToAction("Index", "FileUpload");
        }

        public FileResult DownloadFile(string FileName)
        {
            //Build the File Path.
            string path = Path.Combine(this.webHostEnvironment.WebRootPath, "FileFolder/") + FileName;

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", FileName);
        }



    }
}
