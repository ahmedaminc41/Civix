using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Civix.App.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Civix.App.Core.Service.Contracts.Images
{
    public interface IFileManager
    {
       Task<List<IssueImage>> UploadImages(List<IFormFile> images, string issueId);
    }
}
