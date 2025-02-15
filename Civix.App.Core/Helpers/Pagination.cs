using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Civix.App.Core.Helpers
{
    public class Pagination<T>
    {
        public Pagination(int pageSize, int pageIndex, int count, IEnumerable<T> data, int totatIssues)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            Count = count;
            Data = data;
            TotatIssues = totatIssues;
        }

        public int TotatIssues { get; set; }
        public int PageSize { get; set; } 
        public int PageIndex { get; set; } 
        public int Count { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
