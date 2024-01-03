
namespace MetadataChangerApp.Models
{
    public class FileInfo
    {
        public string FileName { get; set; } = null!;
        public DateTime CreationTime { get; set; }
        public DateTime LastWriteTime { get; set; }

    }
}
