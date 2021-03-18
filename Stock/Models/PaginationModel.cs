using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.Models
{
    public class PaginationModel
    {
        public int Limit { get; set; }

        public int Offset { get; set; }

        public int Count { get; set; }

        public int Total { get; set; }
    }
}
