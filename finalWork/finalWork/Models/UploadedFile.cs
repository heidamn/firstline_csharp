using System.Web;

namespace finalWork.Models
{
    public class UploadedFile
    {
        public HttpPostedFileBase UploadedFileValue { get; set; }
        public string KeyFile { get; set; }
        public string FileName { get; set; }
        public UploadedFile(HttpPostedFileBase fileValue, string key, string name)
        {
            UploadedFileValue = fileValue;
            KeyFile = key;
            FileName = name;
        }
        public UploadedFile() { }
    }
}
