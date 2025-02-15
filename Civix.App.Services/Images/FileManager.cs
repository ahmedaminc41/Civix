using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Civix.App.Core.Entities;
using Civix.App.Core.Service.Contracts.Images;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Civix.App.Services.Images
{
    public class FileManager : IFileManager
    {
        private readonly IWebHostEnvironment _environment;

        public FileManager(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<List<IssueImage>> UploadImages(List<IFormFile> images, string issueId)
        {
            var uploadedImages = new List<IssueImage>();

            if (images == null || images.Count == 0) return uploadedImages;

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            foreach (var image in images)
            {
                var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                var imageUrl = $"/uploads/{uniqueFileName}";
                uploadedImages.Add(new IssueImage { ImageUrl = imageUrl, IssueId = issueId });
            }

            return uploadedImages;
        }
    }
}
