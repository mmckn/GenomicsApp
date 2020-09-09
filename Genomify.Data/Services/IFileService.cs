using AuthorTest.Areas.Identity.Data;
using AuthorTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Storage.Blob;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorTest.Services
{

    //describes the methods that FileService must Implement
    public interface IFileService
    {
        Task<bool> UploadGenome(IFormFile files);
        Task<CloudBlockBlob> DownloadGenome(string GenomeName);
        Task<FileDataViewModel> GetGenome(string contributorId);
        Task<bool> DeleteGenome(string GenomeName);
    }
}
