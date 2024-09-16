using MonPointOfSaleFinal.App.Intefaces;

namespace MonPointOfSaleFinal.App.Repositories
{
    public class UploadFile : IUploudFile
    {
        private IWebHostEnvironment _environment;

        public UploadFile(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> UploadFileAsync(string filePath, IFormFile file)
        {
            string upLoadFolder = _environment.WebRootPath + filePath;
            if (Directory.Exists(upLoadFolder) == false)
            {
                Directory.CreateDirectory(upLoadFolder);
            }
            string UniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string FullPath = Path.Combine(upLoadFolder, UniqueFileName);

            using (var stream = new FileStream(FullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                stream.Dispose();
            }
            return filePath + UniqueFileName;
        }
    }
}
