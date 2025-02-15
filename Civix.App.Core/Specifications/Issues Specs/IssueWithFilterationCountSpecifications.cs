using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Civix.App.Core.Entities;

namespace Civix.App.Core.Specifications.Issues_Specs
{
    public class IssueWithFilterationCountSpecifications : BaseSpecifications<Issue,string>
    {
        public IssueWithFilterationCountSpecifications(IssueSpecParams issueSpec)
            : base(I =>
                  (string.IsNullOrEmpty(issueSpec.Search) || I.Title.ToLower().Contains(issueSpec.Search)) &&
                (!issueSpec.CategoryId.HasValue || I.CategoryId == issueSpec.CategoryId) &&
                (!issueSpec.Status.HasValue || I.Status == issueSpec.Status) &&
                (!issueSpec.Priority.HasValue || I.Priority == issueSpec.Priority)
            )
        {
        }
    }

}
