using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Civix.App.Core.Entities;

namespace Civix.App.Core.Dtos.Issue
{
    public class IssueToReturn
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public string Priority { get; set; }
        public string Status { get; set; } 

        public string Category { get; set; }

        public string CreatedById { get; set; } 
        public DateTime CreatedOn { get; set; } 
        public string? UpdatedById { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
