using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Civix.App.Core.Entities
{
    public enum IssueStatus
    {
        [EnumMember(Value = "Open")]
        Open = 1,
        
        
        [EnumMember(Value = "InProgress")]
        InProgress = 2,
        
        
        [EnumMember(Value = "Resolved")]
        Resolved = 3,

        [EnumMember(Value = "Closed")]
        Closed = 4

    }

}
