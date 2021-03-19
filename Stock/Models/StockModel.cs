using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.Models
{
    public class StockModel
    {
        [FromHeader]
        public string Open { get; set; }

        [FromHeader]
        public string High { get; set; }

        [FromHeader]
        public string Low { get; set; }

        [FromHeader]
        public string Last { get; set; }

        [FromHeader]
        public string Close { get; set; }

        [FromHeader]
        public string Volume { get; set; }

        [FromHeader]
        public string Date { get; set; }

        [FromHeader]
        public string Symbol { get; set; }

        [FromHeader]
        public string Exchange { get; set; }
    }
}
