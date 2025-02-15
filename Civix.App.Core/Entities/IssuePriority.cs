using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Civix.App.Core.Entities
{
    public enum IssuePriority
    {
        [EnumMember(Value = "Low")]
        Low = 1,

        [EnumMember(Value = "Medium")]
        Medium = 2,


        [EnumMember(Value = "High")]
        High = 3,

        [EnumMember(Value = "Critical")]
        Critical = 4
    }

}
