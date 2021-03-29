using Microsoft.AspNetCore.Http;

namespace Core.Utilities.FileSystem
{
    public interface IFileSystem
    {
        string Add(IFormFile file, string path);
        string Update(string pathToUpdate, IFormFile file, string path);
        void Delete(string path);
    }
}