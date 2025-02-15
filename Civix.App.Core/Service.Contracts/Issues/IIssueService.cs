using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Civix.App.Core.Dtos.Issue;
using Civix.App.Core.Entities;

namespace Civix.App.Core.Service.Contracts.Issues
{
    public interface IIssueService
    {
        Task<IEnumerable<IssueToReturn>?> GetAllIssuesAsync();
        Task<IssueToReturn?> CreateIssue(CreateIssueDto issue);
    }
}
