namespace MongoDb.Services.GoogleStorageServices
{
    public interface IGoogleStorageService
    {
        Task<string> UploadFileAsync(IFormFile file, string bucketName, string folderName);
    }
}
