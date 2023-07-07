using Microsoft.AspNetCore.Mvc;
using MVC.Boilerplate.Models.FileUpload;

namespace MVC.Boilerplate.Components
{
    public class FileUploadViewComponent : ViewComponent
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<FileUploadViewComponent> _logger;

        public FileUploadViewComponent(IConfiguration configuration, ILogger<FileUploadViewComponent> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IViewComponentResult Invoke(string FieldName)
        {
            _logger.LogInformation("File Upload View Component");
            ViewBag.AllowedEx = _configuration.GetSection("FileUploadSettings").GetSection("AllowedFileExtension").Value;
            ViewBag.Size = _configuration.GetSection("FileUploadSettings").GetSection("MaxFileSizeMb").Value;
            ViewBag.FileErrMess = _configuration.GetSection("FileUploadSettings").GetSection("FileNotAllowedErrorMessage").Value;
            ViewBag.SizeErrMess = _configuration.GetSection("FileUploadSettings").GetSection("FileSizeExceedErrorMessage").Value;
            return View("FileUpload", FieldName);
        }
    }
}
