using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furlaneti.Finance.ChangeBase.Model
{
    public class ValuationIndicators
    {
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public decimal DividendYield { get; set; }
        public decimal PriceProfit { get; set; }
        public decimal PriceOverAssetValue { get; set; }
        public decimal ReturnOnEquity { get; set; }
    }
}
