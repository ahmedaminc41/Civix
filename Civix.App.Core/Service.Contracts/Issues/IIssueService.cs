using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Civix.App.Core.Dtos.Issue;
using Civix.App.Core.Entities;
using Civix.App.Core.Helpers;
using Civix.App.Core.Specifications.Issues_Specs;

namespace Civix.App.Core.Service.Contracts.Issues
{
    public interface IIssueService
    {
        Task<IEnumerable<IssueToReturn>?> GetAllIssuesAsync();
        Task<Pagination<IssueToReturn>?> GetAllIssuesAsyncWithSpec(IssueSpecParams specParams);
        Task<IssueToReturn?> CreateIssue(CreateIssueDto issue);
    }
}
