namespace MonPointOfSaleFinal.App.Intefaces
{
    public interface IUploudFile
    {
        Task<string> UploadFileAsync(string filePath, IFormFile file);
    }
}
