using MetadataChangerApp.Services;
using Microsoft.Win32;
using System.Windows;

namespace MetadataChangerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileService fileService;
        private string? FocusedPath { get; set; }
        

        public MainWindow()
        {
            InitializeComponent();
            fileService = new FileService();
        }

        private void ChooseFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Tüm Dosyalar|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                FocusedPath = openFileDialog.FileName;
                MetadataChangerApp.Models.FileInfo dosyaBilgisi = fileService.GetFileInfo(FocusedPath);
                dosyaAdiTextBlock.Text = $"Dosya Adı: {dosyaBilgisi.FileName}";
                olusturulmaTarihiTextBlock.Text = $"Oluşturulma Tarihi: {dosyaBilgisi.CreationTime}";
                degistirilmeTarihiTextBlock.Text = $"Değiştirilme Tarihi: {dosyaBilgisi.LastWriteTime}";

                // Diğer meta bilgilerini görüntüleme işlemleri
            }
        }

        /// <summary>
        /// Seçilen dosyanın meta bilgilerini değiştirme işlemleri burada yapılacak
        /// </summary>
        private void Change_Click(object sender, RoutedEventArgs e)
        {
            if (FocusedPath is null)
            {
                MessageBox.Show("Seçili Dosya Yok!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MetadataChangerApp.Models.FileInfo newInfo = new()
            {
                // Eğer MetadataChangerApp.Models.FileInfo sınıfına CreationTime ve LastWriteTime eklediyseniz bu değerleri de set etmelisiniz.
                CreationTime = olusturulmaTarihiDatePicker.DisplayDate,
                LastWriteTime = degistirilmeTarihiDatePicker.DisplayDate
            };

            try
            {
                fileService.ChangeFileInfo(FocusedPath, newInfo);
                MessageBox.Show("Meta bilgileri başarıyla değiştirildi.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata oluştu: {ex.Message}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}