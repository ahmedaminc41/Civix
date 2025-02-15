using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Civix.App.Core.Entities
{
    public class IssueImage : BaseEntity<int>
    {
        public string ImageUrl { get; set; } = string.Empty;
        public string IssueId { get; set; }
        public Issue Issue { get; set; }
    }

}
