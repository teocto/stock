using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.Models
{
    public class StockModel
    {
        public string Open { get; set; }

        public string High { get; set; }

        public string Low { get; set; }

        public string Last { get; set; }

        public string Close { get; set; }

        public string Volume { get; set; }

        public string Date { get; set; }

        public string Symbol { get; set; }

        public string Exchange { get; set; }
    }
}
