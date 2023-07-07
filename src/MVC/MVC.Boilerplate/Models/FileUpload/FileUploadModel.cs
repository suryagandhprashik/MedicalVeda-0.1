namespace MVC.Boilerplate.Models.FileUpload
{
    public class FileUploadModel
    {
        public IFormFile File { get; set; }
        public string FileUrl { get; set; }
        public string FileAllowedExtension { get; set; }
        public string FileSize { get; set; }
    }
}
