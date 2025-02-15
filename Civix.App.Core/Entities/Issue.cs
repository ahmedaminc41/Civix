using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Civix.App.Core.Entities
{
    public class Issue: BaseEntity<string>
    {
        public Issue()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Title { get; set; }
        public string Description { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public IssuePriority Priority { get; set; } = IssuePriority.Low;
        public IssueStatus Status { get; set; } = IssueStatus.Open;

        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public List<IssueImage> Images { get; set; }

    }
}
