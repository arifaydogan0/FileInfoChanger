using System.IO;

namespace MetadataChangerApp.Services
{
    public class FileService
    {
        public MetadataChangerApp.Models.FileInfo GetFileInfo(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            // Dosya ile ilgili meta bilgilerini okuma işlemleri burada yapılacak
            return new MetadataChangerApp.Models.FileInfo()
            {
                FileName = Path.GetFileName(filePath),
                CreationTime = fileInfo.CreationTime,
                LastWriteTime = fileInfo.LastWriteTime
                // Diğer meta bilgilerini ekleyin
            };
        }

        public void ChangeFileInfo(string filePath, MetadataChangerApp.Models.FileInfo newFileInfo)
        {
            // Seçilen dosyanın meta bilgilerini değiştirme işlemleri burada yapılacak
            File.SetCreationTime(filePath, newFileInfo.CreationTime);
            File.SetLastWriteTime(filePath, newFileInfo.LastWriteTime);
            // Diğer meta bilgilerini güncelleme işlemleri
        }
    }
}
