using System.IO;

namespace MetadataChangerApp.Services
{
    public class FileService
    {
        /// <summary>
        /// Dosya ile ilgili meta bilgilerini okuma işlemleri burada yapılacak
        /// </summary>
        public MetadataChangerApp.Models.FileInfo GetFileInfo(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            
            return new MetadataChangerApp.Models.FileInfo()
            {
                FileName = Path.GetFileName(filePath),
                CreationTime = fileInfo.CreationTime,
                LastWriteTime = fileInfo.LastWriteTime
            };
        }

        /// <summary>
        /// Seçilen dosyanın meta bilgilerini değiştirme işlemleri burada yapılacak
        /// </summary>
        public void ChangeFileInfo(string filePath, MetadataChangerApp.Models.FileInfo newFileInfo)
        {
            
            File.SetCreationTime(filePath, newFileInfo.CreationTime);
            File.SetLastWriteTime(filePath, newFileInfo.LastWriteTime);
        }
    }
}
