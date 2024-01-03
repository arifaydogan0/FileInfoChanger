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

            try
            {
                MetadataChangerApp.Models.FileInfo fileInfo = fileService.GetFileInfo(FocusedPath);

                // Seçilen dosyanın creation zamanının saat, dakika, saniye, milisaniye vs bilgilerini değiştirmeyerek al
                DateTime newCreationTime = new DateTime(
                    olusturulmaTarihiDatePicker.SelectedDate!.Value.Year,
                    olusturulmaTarihiDatePicker.SelectedDate.Value.Month,
                    olusturulmaTarihiDatePicker.SelectedDate.Value.Day,
                    fileInfo.CreationTime.Hour,
                    fileInfo.CreationTime.Minute,
                    fileInfo.CreationTime.Second,
                    fileInfo.CreationTime.Millisecond
                );

                // Seçilen dosyanın lastwritetime'ını güncellenen creationtime'ın bir saniye sonrası olarak ayarla
                DateTime newLastWriteTime = newCreationTime.AddSeconds(1);

                MetadataChangerApp.Models.FileInfo newInfo = new()
                {
                    CreationTime = newCreationTime,
                    LastWriteTime = newLastWriteTime
                };

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