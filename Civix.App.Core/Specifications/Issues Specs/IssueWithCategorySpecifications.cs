using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Civix.App.Core.Entities;

namespace Civix.App.Core.Specifications.Issues_Specs
{
    public class IssueWithCategorySpecifications : BaseSpecifications<Issue, string>
    {
        public IssueWithCategorySpecifications(IssueSpecParams issueSpec)
            : base(I =>
                (string.IsNullOrEmpty(issueSpec.Search) || I.Title.ToLower().Contains(issueSpec.Search)) &&
                (!issueSpec.CategoryId.HasValue || I.CategoryId == issueSpec.CategoryId) &&
                (!issueSpec.Status.HasValue || I.Status == issueSpec.Status) &&
                (!issueSpec.Priority.HasValue || I.Priority == issueSpec.Priority)
            )
        {
            Includes.Add(I => I.Category);
            Includes.Add(I => I.Images);

            if (!string.IsNullOrEmpty(issueSpec.Sort))
            {
                switch (issueSpec.Sort.ToLower())
                {
                    case "priorityasc":
                        AddOrderBy(I => I.Priority);
                        break;
                    case "prioritydesc":
                        AddOrderByDesc(I => I.Priority);
                        break;
                    case "statusasc":
                        AddOrderBy(I => I.Status);
                        break;
                    case "statusdesc":
                        AddOrderByDesc(I => I.Status);
                        break;
                    case "titleasc":
                        AddOrderBy(I => I.Title);
                        break;
                    case "titledesc":
                        AddOrderByDesc(I => I.Title);
                        break;
                    case "dateasc":
                        AddOrderBy(I => I.CreatedOn);
                        break;
                    case "datedesc":
                        AddOrderByDesc(I => I.CreatedOn);
                        break;
                    default:
                        AddOrderBy(I => I.CreatedOn); // Default sorting by creation date
                        break;
                }
            }
            else
            {
                AddOrderByDesc(I => I.CreatedOn); // Default sorting
            }

            // Apply pagination
            ApplyPagination((issueSpec.PageIndex - 1) * issueSpec.PageSize, issueSpec.PageSize);
        }
    }


}
