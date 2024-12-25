
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace MongoDb.Services.GoogleStorageServices
{
    public class GoogleStorageService : IGoogleStorageService
    {
        private readonly string _credentialsPath;

        public GoogleStorageService()
        {
            // wwwroot/config/google-credentials.json dosyasının yolunu alıyoruz
            _credentialsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "config", "google-credentials.json");
        }
        public async Task<string> UploadFileAsync(IFormFile file, string bucketName, string folderName)
        {
            if (file == null || file.Length <= 0)
            {
                throw new ArgumentException("Geçerli bir dosya yüklenmedi.");
            }

            // Google Cloud Storage istemcisini oluştur
            var credential = GoogleCredential.FromFile(_credentialsPath);
            var storageClient = StorageClient.Create(credential);

            // Dosya adı ve Google Storage'deki tam yolu belirle
            var fileName = $"{folderName}/{Guid.NewGuid()}_{file.FileName}";
            var objectName = $"{fileName}";

            // Dosyayı geçici bir belleğe kopyala
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                // Dosyayı Google Storage'a yükle
                await storageClient.UploadObjectAsync(bucketName, objectName, file.ContentType, memoryStream);
            }

            // Yüklenen dosyanın erişim URL'sini döndür
            return $"https://storage.googleapis.com/{bucketName}/{objectName}";
        }
    }
}
