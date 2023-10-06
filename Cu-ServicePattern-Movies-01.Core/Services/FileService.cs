using Cu_ServicePattern_Movies_01.Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Cu_ServicePattern_Movies_01
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public bool Delete(string filename)
        {
            //delete the old file
            var pathToImages = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            try
            {
                File.Delete(Path.Combine(pathToImages, filename));
            }
            catch(FileNotFoundException fileNotFoundException)
            {
                Console.WriteLine(fileNotFoundException.Message);
                return false;
            }
            return true;
        }

        public async Task<string> Store(IFormFile file)
        {
            //1 create unique filename
            var filename = $"{Guid.NewGuid().ToString()}_{file.FileName}";
            //2 create path to image
            var pathToImages = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            if (!Directory.Exists(pathToImages))
            {
                Directory.CreateDirectory(pathToImages);
            }
            var fullPathToFile = Path.Combine(pathToImages, filename);
            //3 copy image to disk
            using (FileStream fileStream = new FileStream(fullPathToFile, FileMode.Create))
            {
                //copy
                await file.CopyToAsync(fileStream);
            }
            //4 return imagename to store in database
            return filename;
        }

        public async Task<string> Update(IFormFile file, string oldFilename)
        {
            if(Delete(oldFilename))
            {
                return await Store(file);
            }
            return "Error";
        }
    }
}
