using Microsoft.AspNetCore.Http;

namespace Cu_ServicePattern_Movies_01.Core.Interfaces
{
    public interface IFileService
    {
        Task<string> Store(IFormFile file);
        bool Delete(string filename);
        Task<string> Update(IFormFile file,string oldFilename);
    }
}
