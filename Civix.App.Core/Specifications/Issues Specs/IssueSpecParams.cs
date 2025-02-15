using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Civix.App.Core.Entities;

namespace Civix.App.Core.Specifications.Issues_Specs
{
    public class IssueSpecParams
    {
        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }

        private const int MaxPageSize = 20;  // Increased max page size for flexibility
        private int pageSize = 5;           // Default page size

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        public int PageIndex { get; set; } = 1;

        // Filtering Options
        public int? CategoryId { get; set; }
        public IssueStatus? Status { get; set; }    // Enum for status
        public IssuePriority? Priority { get; set; }  // Enum for priority

        // Sorting Options
        public string? Sort { get; set; }
    }

}
