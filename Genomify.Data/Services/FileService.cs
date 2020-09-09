using AuthorTest.Areas.Identity.Data;
using AuthorTest.Data;
using AuthorTest.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorTest.Services
{


    // This file handles all interaction between Azure blob storage and the web application.
    public class FileService : IFileService
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AuthorTestUser> userManager;
        private readonly AuthorDbContext authorDbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly SignInManager<AuthorTestUser> signInManager;
        private readonly DataSeeder dataSeeder;
        private readonly IConfiguration configuration;

        public FileService(RoleManager<IdentityRole> roleManager, UserManager<AuthorTestUser> userManager, IHttpContextAccessor httpContextAccessor, AuthorDbContext authorDbContext, SignInManager<AuthorTestUser> signInManager, IConfiguration configuration)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.authorDbContext = authorDbContext;
            this.httpContextAccessor = httpContextAccessor;
            this.signInManager = signInManager;
            dataSeeder = new DataSeeder();

            this.configuration = configuration;

        }


        public async Task<bool> UploadGenome(IFormFile file)
        {

            var fileType = Path.GetExtension(file.FileName);
            if (fileType.Equals(".fasta") || fileType.Equals(".VCF"))
            {
                string systemFileName = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                string blobstorageconnection = configuration.GetValue<string>("blobstorage");

                // Retrieve storage account from connection string.
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);

                // Create the blob client.
                CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();

                // Retrieve a reference to a container.
                CloudBlobContainer container = blobClient.GetContainerReference("genomecontainer");

                CloudBlockBlob blockBlob = container.GetBlockBlobReference(systemFileName);
                await using (var data = file.OpenReadStream())
                {
                    await blockBlob.UploadFromStreamAsync(data);

                }
                return true;
            }
            return false;
        }


        public async Task<CloudBlockBlob> DownloadGenome(string GenomeName)
        {
            CloudBlockBlob blockBlob;
            await using (MemoryStream memoryStream = new MemoryStream())
            {
                string blobstorageconnection = configuration.GetValue<string>("blobstorage");
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("genomecontainer");
                blockBlob = cloudBlobContainer.GetBlockBlobReference(GenomeName);
                await blockBlob.DownloadToStreamAsync(memoryStream);
            }

            Stream blobStream = blockBlob.OpenReadAsync().Result;
            return blockBlob;

        }


        public async Task<bool> DeleteGenome(string GenomeName)
        {
            string blobstorageconnection = configuration.GetValue<string>("blobstorage");
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("genomecontainer");
            var blockBlob = cloudBlobContainer.GetBlobReference(GenomeName);
            var checkDeleted = await blockBlob.DeleteIfExistsAsync();

            return checkDeleted;
        }


        public async Task<FileDataViewModel> GetGenome(string contributorId)
        {

            string blobstorageconnection = configuration.GetValue<string>("blobstorage");

            // Retrieve storage account from connection string.
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);

            //Create the blob client
            CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();

            //get access to the container where genome files are stored
            CloudBlobContainer container = blobClient.GetContainerReference("genomecontainer");

            FileDataViewModel file = new FileDataViewModel();

            var found = container.GetBlobReference(contributorId);

            // If file exists.
            if (await found.ExistsAsync())
            {
                file.FileName = found.Name;

                return file;
            }

            return null;
        }
    }
}
