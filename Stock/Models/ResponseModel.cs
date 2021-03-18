using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.Models
{
    public class ResponseModel
    {
        public PaginationModel pagination { get; set; }

        public List<StockModel> data { get; set; }
    }
}
