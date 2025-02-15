using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Civix.App.Core.Entities
{
    public class BaseEntity<TKey> : AuditableEntity
    {
        public TKey Id { get; set; }
    }
}
